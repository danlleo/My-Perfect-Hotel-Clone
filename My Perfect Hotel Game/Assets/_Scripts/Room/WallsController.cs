using UnityEngine;
using Utilities;

namespace Room
{
    [RequireComponent(typeof(Room))]
    public class WallsController : MonoBehaviour
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
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            EditorValidation.AreEnumerableValues(this, nameof(_wallsToRemoveOnUpgrade), _wallsToRemoveOnUpgrade);
        }
#endif

        public void SetWallsActive(bool isActive)
        {
            foreach (Transform wall in _wallsToRemoveOnUpgrade)
            {
                if (wall != null)
                    wall.gameObject.SetActive(isActive);
            }
        }
    }
}
