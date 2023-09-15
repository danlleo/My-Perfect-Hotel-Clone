using System.Collections.Generic;
using Guest.StateMachine;
using Guest.StateMachine.States;

namespace Guest
{
    public class GuestStateFactory
    {
        private readonly Dictionary<State, GuestState> _states = new();

        public GuestStateFactory(GuestStateManager currentContext)
        {
            _states[State.Sleeping] = new SleepingOnBed(currentContext, this);
            _states[State.Waiting] = new WaitingInReceptionLine(currentContext, this);
            _states[State.WalkingToReceptionQueueLine] = new WalkingToReceptionQueueLine(currentContext, this);
            _states[State.WalkingToRoomBed] = new WalkingToRoomBed(currentContext, this);
            _states[State.WalkingToTaxi] = new WalkingToTaxi(currentContext, this);
        }

        public GuestState Sleeping() => _states[State.Sleeping];
        public GuestState Waiting() => _states[State.Waiting];
        public GuestState WalkingToReceptionQueueLine() => _states[State.WalkingToReceptionQueueLine];
        public GuestState WalkingToRoomBed() => _states[State.WalkingToRoomBed];
        public GuestState WalkingToTaxi() => _states[State.WalkingToTaxi];

        private enum State
        {
            Sleeping,
            Waiting,
            WalkingToReceptionQueueLine,
            WalkingToRoomBed,
            WalkingToTaxi
        }
    }
}
