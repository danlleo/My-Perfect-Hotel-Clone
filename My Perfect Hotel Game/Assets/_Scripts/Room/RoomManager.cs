using System.Collections.Generic;
using Misc;
using UnityEngine;

namespace Room
{
    public class RoomManager : Singleton<RoomManager>
    {
        [Tooltip("Populate with default rooms that are available when player starts a new game")]
        [SerializeField] private List<Room> _availableRooms;

        public Room GetAvailableRoom()
        {
            foreach (var room in _availableRooms)
            {
                if (room.IsAvailable)
                    return room;
            }
            
            return null;
        }

        public bool HasAvailableRoom()
        {
            foreach (var room in _availableRooms)
            {
                if (room.IsAvailable)
                    return true;
            }

            return false;
        }

        public bool TryGetUncleanRoom(out Room uncleanRoom)
        {
            uncleanRoom = null;
            
            foreach (var room in _availableRooms)
            {
                if (!room.IsRoomUnclean() || room.HasMaidOccupied()) continue;
                
                print("Unclean room: " + room.name);
                
                uncleanRoom = room;
                return true;
            }

            return false;
        }
    }
}
