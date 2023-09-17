using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace UI.Helpers
{
    public class ProgressBarUI : MonoBehaviour
    {
        [SerializeField] private Image _background;
        
        public void UpdateProgressBar(float timer, float maxTime)
        {
            float t = timer / maxTime;

            _background.fillAmount = t;
        }

        public void ClearProgressBar()
            => _background.fillAmount = 0;

        #region Validation

#if UNITY_EDITOR
        private void OnValidate()
        {
            EditorValidation.IsNullValue(this, nameof(_background), _background);
        }
#endif        

        #endregion
    }
}
