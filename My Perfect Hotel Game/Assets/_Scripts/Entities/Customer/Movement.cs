using UnityEngine;
using UnityEngine.AI;
using Utilities;

namespace Entities.Customer
{
    [SelectionBase] [RequireComponent(typeof(NavMeshAgent))] [DisallowMultipleComponent]
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

#if UNITY_EDITOR
        private void OnValidate()
        {
            EditorValidation.IsPositiveValue(this, nameof(_movementSpeed), _movementSpeed);
            EditorValidation.IsPositiveValue(this, nameof(_stoppingDistance), _stoppingDistance);
        }
#endif

        public void MoveTo(Vector3 destination)
        {
            _navMeshAgent.SetDestination(destination);
        }

        private void SetAgentMovementSpeed(float movementSpeed) => _navMeshAgent.speed = movementSpeed;

        private void SetAgentStoppingDistance(float stoppingDistance) => _navMeshAgent.stoppingDistance = stoppingDistance;
    }
}
