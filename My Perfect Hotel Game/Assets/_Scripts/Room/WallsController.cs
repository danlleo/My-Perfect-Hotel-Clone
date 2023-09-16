using UnityEngine;

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
