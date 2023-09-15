using Guest.StateMachine;
using Guest.StateMachine.States;

namespace Guest
{
        public class GuestStateFactory
        {
            private GuestStateManager _context;

            public GuestStateFactory(GuestStateManager currentContext)
            {
                _context = currentContext;
            }

            public GuestState Sleeping() => new SleepingOnBed(_context, this);
            public GuestState Waiting() => new WaitingInReceptionLine(_context, this);
            public GuestState WalkingToReceptionQueueLine() => new WalkingToReceptionQueueLine(_context, this);
            public GuestState WalkingToRoomBed() => new WalkingToRoomBed(_context, this);
            public GuestState WalkingToTaxi() => new WalkingToTaxi(_context, this);
        }
}
