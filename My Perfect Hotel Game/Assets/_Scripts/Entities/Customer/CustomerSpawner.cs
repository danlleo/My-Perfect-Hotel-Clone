using Events;
using QueueLines.ReceptionQueueLine;
using UnityEngine;
using UnityEngine.Serialization;
using Utilities;

namespace Entities.Customer
{
    [DisallowMultipleComponent] public class CustomerSpawner : MonoBehaviour
    {
        // Hardcoded for now
        [SerializeField] private ReceptionQueueLine _receptionQueueLine;
        [FormerlySerializedAs("_guestPrefab")] [SerializeField]
        private Customer _customerPrefab;

        [SerializeField] private Transform _spawnPoint;

        [SerializeField] private float _delaySpawnTime = 1.35f;

        private float _timer;

        private void Update()
        {
            _timer += Time.deltaTime;

            if (!(_timer >= _delaySpawnTime)) return;

            ResetTimer();

            // Check if the line is not full
            if (_receptionQueueLine.IsLineFull())
                return;

            Spawn();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            EditorValidation.IsNullValue(this, nameof(_receptionQueueLine), _receptionQueueLine);
            EditorValidation.IsNullValue(this, nameof(_customerPrefab), _customerPrefab);
            EditorValidation.IsNullValue(this, nameof(_spawnPoint), _spawnPoint);
            EditorValidation.IsPositiveValue(this, nameof(_delaySpawnTime), _delaySpawnTime);
        }
#endif

        private void Spawn()
        {
            Customer guest = Instantiate(_customerPrefab, _spawnPoint.position, Quaternion.identity);

            guest.Initialize(_receptionQueueLine, _receptionQueueLine.GetWorldPositionToStayInLine(),
                transform.position + Vector3.right * 2f);

            _receptionQueueLine.CustomerSpawnedEvent.Call(this, new CustomerSpawnedEventArgs(guest));
        }

        private void ResetTimer() => _timer = 0f;
    }
}
