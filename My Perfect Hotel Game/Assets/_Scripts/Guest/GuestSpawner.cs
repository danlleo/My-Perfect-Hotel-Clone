using Events;
using QueueLines.ReceptionQueueLine;
using UnityEngine;

namespace Guest
{
    [DisallowMultipleComponent]
    public class GuestSpawner : MonoBehaviour
    {
        // Hardcoded for now
        [SerializeField] private ReceptionQueueLine _receptionQueueLine;
        [SerializeField] private Guest _guestPrefab;

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

        private void Spawn()
        {
            var guest = Instantiate(_guestPrefab, _spawnPoint.position, Quaternion.identity);
            guest.Initialize(_receptionQueueLine, _receptionQueueLine.GetWorldPositionToStayInLine(), transform.position);
            
            _receptionQueueLine.GuestSpawnedEvent.Call(this, new GuestSpawnedEventArgs(guest: guest));
        }

        private void ResetTimer()
            => _timer = 0f;
    }
}
