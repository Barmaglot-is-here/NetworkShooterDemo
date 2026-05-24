using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.Services.PlayerTab;
using Assets.Game.Scripts.Services.ProfileManagement;
using Assets.Game.Scripts.Utils;
using Assets.Game.Scripts.Utils.Synchronization;
using Signals;
using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerTabService : NetworkVariableBase
{
    private readonly ProfileManager _profileManager;
    private readonly PlayerList _playerList;

    private readonly List<PlayerTabData> _dataList;

    private (DeltaType, PlayerTabData) _delta;

    public IReadOnlyList<PlayerTabData> DataList => _dataList;

    public event Action<PlayerTabData> OnAdd;
    public event Action<PlayerTabData> OnRemove;

    private readonly ReadySignal _readySignal;
    public IReadySignal ReadySignal => _readySignal;

    public PlayerTabService(PlayerList playerList, ProfileManager profileManager)
    {
        _readySignal    = new();
        _playerList     = playerList;
        _profileManager = profileManager;

        _dataList = new();

        _playerList.OnAdd += OnPlayerAdd;
        _playerList.OnRemove += OnPlayerRemove;
    }

    public override void OnInitialize()
    {
        base.OnInitialize();

        _readySignal.SetReady();
    }

    private void OnPlayerAdd(ulong id, int team)
    {
        PlayerTabData data = new(id, team, _profileManager.Profiles[id].PlayerName);

        AddWithNotify(data);

        _delta = new(DeltaType.Add, data);

        SetDirty(true);
    }

    private void AddWithNotify(PlayerTabData data)
    {
        _dataList.Add(data);

        OnAdd?.Invoke(data);
    }

    private void OnPlayerRemove(ulong id, int team)
    {
        var data = _dataList.Find(p => p.Id == id);

        RemoveWithNotify(data);

        _delta = new(DeltaType.Remove, data);

        SetDirty(true);
    }

    private void RemoveWithNotify(PlayerTabData data)
    {
        _dataList.Remove(data);

        OnRemove?.Invoke(data);
    }

    public override void Dispose()
    {
        base.Dispose();

        _playerList.OnAdd -= OnPlayerAdd;
        _playerList.OnRemove -= OnPlayerRemove;
    }

    public override void WriteDelta(FastBufferWriter writer)
    {
        var json = JsonUtility.ToJson(_delta);

        writer.WriteValueSafe(json);
    }

    public override void WriteField(FastBufferWriter writer)
    {
        var json = JsonUtility.ToJson(new SerializableList<PlayerTabData>(_dataList));

        writer.WriteValueSafe(json);
    }

    public override void ReadField(FastBufferReader reader)
    {
        reader.ReadValueSafe(out string json);

        var list = JsonUtility.FromJson<SerializableList<PlayerTabData>>(json).Value;

        foreach (var data in list)
            AddWithNotify(data);
    }

    public override void ReadDelta(FastBufferReader reader, bool keepDirtyDelta)
    {
        reader.ReadValueSafe(out string json);

        var delta = JsonUtility.FromJson<(DeltaType, PlayerTabData)>(json);

        if (delta.Item1 == DeltaType.Add)
            AddWithNotify(delta.Item2);
        else
            RemoveWithNotify(delta.Item2);
    }
}
