using System;
using System.Collections.Generic;

namespace Assets.Game.Scripts.Services.ProfileManagement
{
    public class ProfileManager
    {
        private readonly Dictionary<ulong, PlayerProfile> _profiles;

        public IReadOnlyDictionary<ulong, PlayerProfile> Profiles => _profiles;

        public static PlayerProfile LocalPlayerProfile { get; set; }

        public event Action<ulong> OnAdd;
        public event Action<ulong> OnRemove;

        public ProfileManager()
        {
            _profiles = new();
        }

        public void Add(ulong id, PlayerProfile profile)
        {
            _profiles.Add(id, profile);

            OnAdd?.Invoke(id);
        }

        public PlayerProfile Get(ulong id) => _profiles[id];

        public void Remove(ulong id)
        {
            _profiles.Remove(id);

            OnRemove?.Invoke(id);
        }
    }
}
