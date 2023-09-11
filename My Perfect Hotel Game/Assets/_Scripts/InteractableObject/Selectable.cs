using TMPro;
using UnityEngine;

namespace InteractableObject
{
    [SelectionBase]
    public class Selectable : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _lookPercentageLabel;

        private float _lookPercentage;
     
        private void Update()
            => _lookPercentageLabel.text = _lookPercentage.ToString("F2");

        public void SetLookPercentage(float percentage)
        {
            if (percentage < 0)
                return;

            _lookPercentage = percentage;
        }
    }
}
