using UnityEngine;
using UnityEngine.UI;

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
    }
}
