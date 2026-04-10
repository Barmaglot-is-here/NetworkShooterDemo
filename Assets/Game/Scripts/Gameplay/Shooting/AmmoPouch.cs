using Assets.Rx;
using System;

namespace Assets.Game.Scripts.Gameplay.Shooting
{
    public class AmmoPouch
    {
        private readonly Rx<int> _ammoCount;

        public int AmmoCountMax { get; private set; }
        public IReadOnlyRx<int> AmmoCount => _ammoCount;

        public AmmoPouch(int ammoCount, int ammoCountMax)
        {
            _ammoCount      = new(ammoCount);
            AmmoCountMax    = ammoCountMax;
        }

        public void AddAmmo()
        {
            throw new NotImplementedException();
        }

        public int GetAmmo(int count)
        {
            if (_ammoCount.Value == 0)
                return 0;

            if (count > _ammoCount.Value)
                count = _ammoCount.Value;

            _ammoCount.Value -= count;

            return count;
        }
    }
}
