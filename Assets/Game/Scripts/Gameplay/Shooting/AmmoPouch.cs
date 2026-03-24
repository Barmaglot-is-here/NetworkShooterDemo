using Assets.Rx;

namespace Assets.Game.Scripts.Gameplay.Shooting
{
    public class AmmoPouch
    {
        public Rx<int> AmmoCount { get; set; }
        public int AmmoCountMax { get; private set; }

        public AmmoPouch(int ammoCount, int ammoCountMax)
        {
            AmmoCount       = new(ammoCount);
            AmmoCountMax    = ammoCountMax;
        }
    }
}
