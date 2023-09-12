using Events;

namespace Guest.States
{
    public class BookingRoomState : GuestState
    {
        private GuestStateManager _guestStateManager;
        
        public override void EnterState(GuestStateManager guestStateManager)
        {
            _guestStateManager = guestStateManager;
            guestStateManager.CurrentGuest.GuestSetRoomEvent.OnGuestSetRoom += GuestSetRoomEvent_OnGuestSetRoom;
        }

        public override void UpdateState(GuestStateManager guestStateManager)
        {
            // ...
        }

        public override void LeaveState(GuestStateManager guestStateManager)
        {
            guestStateManager.CurrentGuest.GuestSetRoomEvent.OnGuestSetRoom -= GuestSetRoomEvent_OnGuestSetRoom;
        }

        private void GuestSetRoomEvent_OnGuestSetRoom(GuestSetRoomEventArgs guestSetRoomEventArgs)
        {
            _guestStateManager.SwitchState(_guestStateManager.WalkingToRoomBed);
            
            LeaveState(_guestStateManager);
        }
    }
}
