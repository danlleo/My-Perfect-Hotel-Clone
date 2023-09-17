using UnityEngine;
using Utilities;

namespace Room
{
    [RequireComponent(typeof(Room))]
    public class WallsToggler : MonoBehaviour
    {
        [SerializeField] private Transform[] _wallsToRemoveOnUpgrade;

        private void OnEnable()
        {
            SetWallsActive(false);
        }
        
        private void OnDisable()
        {
            SetWallsActive(true);
        }

        private void SetWallsActive(bool isActive)
        {
            foreach (Transform wall in _wallsToRemoveOnUpgrade)
            {
                if (wall != null)
                    wall.gameObject.SetActive(isActive);
            }
        }

        #region Validation

#if UNITY_EDITOR
        private void OnValidate()
        {
            EditorValidation.AreEnumerableValues(this, nameof(_wallsToRemoveOnUpgrade), _wallsToRemoveOnUpgrade);
        }
#endif

        #endregion
    }
}
