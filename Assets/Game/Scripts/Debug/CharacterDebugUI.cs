using Assets.Game.Scripts.Gameplay;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDebugUI : NetworkBehaviour
{
    [SerializeField]
    private Slider _healthBar;
    [SerializeField]
    private HealthComponent _healthComponent;

    private Camera _camera;

    public override void OnNetworkSpawn()
    {
        //Выключаем UI для владельца, но показываем всем остальным
        if (IsOwner)
        {
            gameObject.SetActive(false);

            return;
        }

        _healthComponent.OnChanged += OnHealthChanged;

        OnHealthChanged(0, _healthComponent.Value);

        _camera = Camera.main;
    }

    //Поворачиваем в сторону смотрящего
    private void LateUpdate()
    {
        transform.rotation = _camera.transform.rotation;
    }

    private void OnHealthChanged(int previousValue, int newValue) 
        => _healthBar.value = newValue;
}
