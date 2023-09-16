using System;
using Events;
using InteractableObject;
using UnityEngine;
using UnityEngine.AI;

namespace Maid
{
    [SelectionBase]
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(MaidRemovedFromRoomEvent))]
    [DisallowMultipleComponent]
    public class Maid : MonoBehaviour
    {
        private NavMeshAgent _navMeshAgent;

        public Movement Movement { get; private set; }

        public Room.Room Room { get; private set; }

        public Interactable ObjectToClean { get; private set; }
        
        public MaidRemovedFromRoomEvent MaidRemovedFromRoomEvent { get; private set; }
        
        private void Awake()
        {
            Movement = GetComponent<Movement>();
            MaidRemovedFromRoomEvent = GetComponent<MaidRemovedFromRoomEvent>();
        }

        private void OnEnable()
        {
            MaidRemovedFromRoomEvent.Event += MaidRemovedFromRoom_Event;
        }

        private void OnDisable()
        {
            MaidRemovedFromRoomEvent.Event -= MaidRemovedFromRoom_Event;
        }

        public void SetRoomForCleaning(Room.Room room)
            => Room = room;

        public bool HasOccupiedRoom()
            => Room != null;

        public void SetObjectToClean(Interactable objectToClean)
            => ObjectToClean = objectToClean;

        public void RemoveObjectToClean()
            => ObjectToClean = null;

        private void RemoveRoomForCleaning()
            => Room = null;
        
        private void MaidRemovedFromRoom_Event(object sender, EventArgs e)
        {
            RemoveRoomForCleaning();
            RemoveObjectToClean();
        }
    }
}
