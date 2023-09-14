using UnityEngine;
using UnityEngine.AI;

namespace Guest
{
    [SelectionBase]
    [RequireComponent(typeof(NavMeshAgent))]
    [DisallowMultipleComponent]
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed = 4.5f;
        [SerializeField] private float _stoppingDistance = .125f;
        
        private NavMeshAgent _navMeshAgent;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            
            SetAgentMovementSpeed(_movementSpeed);
            //SetAgentStoppingDistance(_stoppingDistance);
        }

        public void MoveTo(Vector3 destination)
        {
            _navMeshAgent.SetDestination(destination);
        }
        
        private void SetAgentMovementSpeed(float movementSpeed)
            => _navMeshAgent.speed = movementSpeed;
        
        private void SetAgentStoppingDistance(float stoppingDistance)
            => _navMeshAgent.stoppingDistance = stoppingDistance;
    }
}