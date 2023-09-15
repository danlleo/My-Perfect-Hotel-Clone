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

        public Room.Room Room { get; private set; }

        public void SetRoomForCleaning(Room.Room room)
            => Room = room;
    }
}
