using UnityEngine;

namespace UIManagement.Assets.UIManagement
{
    [DefaultExecutionOrder(-1)]
    public class UIRegistrator : MonoBehaviour
    {
        [SerializeField]
        private UIScreen[] _screens;

        private void Awake()
        {
            foreach (var screen in _screens)
                UIManager.Register(screen);
        }
    }
}
