using System;
using Events;
using InteractableObject;
using UnityEngine;
using UnityEngine.AI;

namespace Entities.Employees.Maid
{
    [SelectionBase]
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(MaidRemovedFromRoomEvent))]
    [DisallowMultipleComponent]
    public class Maid : Employee
    {
        public Movement Movement { get; private set; }

        public Room.Room Room { get; private set; }

        public Interactable ObjectToClean { get; private set; }

        public MaidRemovedFromRoomEvent MaidRemovedFromRoomEvent { get; private set; }

        private NavMeshAgent _navMeshAgent;
        
        private void Awake()
        {
            Movement = GetComponent<Movement>();
            MaidRemovedFromRoomEvent = GetComponent<MaidRemovedFromRoomEvent>();
        }

        public void SetRoomForCleaning(Room.Room room) => Room = room;

        public bool HasOccupiedRoom() => Room != null;

        public void SetObjectToClean(Interactable objectToClean)
            => ObjectToClean = objectToClean;

        public bool HasObjectToClean()
            => ObjectToClean != null;
        
        public void RemoveObjectToClean()
            => ObjectToClean = null;
        
        public void RemoveRoomForCleaning() => Room = null;
        
        protected override Vector3 GetNextDestination() => throw new NotImplementedException();
    }
}
