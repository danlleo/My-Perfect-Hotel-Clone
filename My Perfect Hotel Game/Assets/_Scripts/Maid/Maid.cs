using UnityEngine;
using UnityEngine.AI;

namespace Maid
{
    [SelectionBase]
    [RequireComponent(typeof(Movement))]
    [DisallowMultipleComponent]
    public class Maid : MonoBehaviour
    {
        private NavMeshAgent _navMeshAgent;

        public Movement Movement { get; private set; }
    }
}
