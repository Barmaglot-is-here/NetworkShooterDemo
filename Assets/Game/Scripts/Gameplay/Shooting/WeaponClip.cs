using Assets.Rx;

namespace Assets.Game.Scripts.Gameplay.Shooting
{
    public class WeaponClip
    {
        public Rx<int> AmmoCount { get; private set; }
        public int AmmoCountMax { get; private set; }

        public bool IsEmpty => AmmoCount.Value == 0;

        public WeaponClip(int ammoCountMax)
        {
            AmmoCount       = new(ammoCountMax);
            AmmoCountMax    = ammoCountMax;

            AmmoCount.Changed += OnCountChanged;
        }

        private void OnCountChanged(int value)
        {
            if (value > AmmoCountMax)
                AmmoCount.Value = AmmoCountMax;
        }
    }
}
