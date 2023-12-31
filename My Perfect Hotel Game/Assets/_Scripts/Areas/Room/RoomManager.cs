using System.Collections.Generic;
using Misc;
using UnityEngine;
using Utilities;

namespace Areas
{
    public class RoomManager : Singleton<RoomManager>
    {
        [Tooltip("Populate with default rooms that are available when player starts a new game")]
        [SerializeField] private List<Room> _availableRooms;

        public Room GetAvailableRoom()
        {
            foreach (Room room in _availableRooms)
            {
                if (room.IsAvailable)
                    return room;
            }
            
            return null;
        }

        public bool HasAvailableRoom()
        {
            foreach (Room room in _availableRooms)
            {
                if (room.IsAvailable)
                    return true;
            }

            return false;
        }

        public bool TryGetUncleanRoom(out Room uncleanRoom)
        {
            uncleanRoom = null;
            
            foreach (Room room in _availableRooms)
            {
                if (!room.IsRoomUnclean() || room.HasMaidOccupied) continue;
                
                uncleanRoom = room;
                return true;
            }

            return false;
        }

        #region Validation

#if UNITY_EDITOR
        private void OnValidate()
        {
            EditorValidation.AreEnumerableValues(this, nameof(_availableRooms), _availableRooms);
        }
#endif

        #endregion
    }
}
