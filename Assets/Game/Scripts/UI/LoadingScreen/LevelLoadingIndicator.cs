using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.UI
{
    internal class LevelLoadingIndicator : MonoBehaviour
    {
        [SerializeField]
        private Slider _loadingSlider;

        private AsyncOperation _loadingOperation;

        public void Bind(AsyncOperation loadingOperation)
        {
            _loadingOperation = loadingOperation;

            StartCoroutine(LoadingCoroutine());
        }

        private IEnumerator LoadingCoroutine()
        {
            do
            {
                _loadingSlider.value = _loadingOperation.progress;

                yield return null;
            } while (!_loadingOperation.isDone);
        }
    }
}
