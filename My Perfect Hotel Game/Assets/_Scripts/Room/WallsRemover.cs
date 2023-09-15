using System.Collections.Generic;
using UnityEngine;

namespace Room
{
    [RequireComponent(typeof(Room))]
    public class WallsRemover : MonoBehaviour
    {
        [SerializeField] private Transform[] _wallsToRemoveOnUpgrade;

        private void Start()
        {
            DisableWalls();
        }

        public void DisableWalls()
        {
            foreach (Transform wall in _wallsToRemoveOnUpgrade)
            {
                if (wall != null)
                    wall.gameObject.SetActive(false);
            }
        }
    }
}
