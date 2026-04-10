using Assets.Rx;
using System;

namespace Assets.Game.Scripts.Gameplay.Shooting
{
    public class WeaponClip
    {
        private readonly Rx<int> _ammoCount;

        public int AmmoCountMax { get; private set; }
        public IReadOnlyRx<int> AmmoCount => _ammoCount;

        public bool IsEmpty => AmmoCount.Value == 0;

        public WeaponClip(int ammoCountMax)
        {
            _ammoCount      = new(ammoCountMax);
            AmmoCountMax    = ammoCountMax;
        }

        public void Load(int ammoCount)
        {
            _ammoCount.Value += ammoCount;

            if (_ammoCount.Value > AmmoCountMax || _ammoCount.Value < 0)
                throw new ArgumentException($"{nameof(ammoCount)}: {ammoCount}");
        }
    }
}
