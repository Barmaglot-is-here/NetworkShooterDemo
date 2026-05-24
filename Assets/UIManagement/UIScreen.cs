using UnityEngine;

namespace UIManagement
{
    public class UIScreen : MonoBehaviour
    {
        internal protected virtual void Show() => gameObject.SetActive(true);
        internal protected virtual void Hide() => gameObject.SetActive(false);
    }
}
