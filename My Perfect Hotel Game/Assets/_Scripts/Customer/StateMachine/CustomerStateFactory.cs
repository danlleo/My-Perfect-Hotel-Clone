using System.Collections.Generic;
using Customer.StateMachine.States;

namespace Customer.StateMachine
{
    public class CustomerStateFactory
    {
        private readonly Dictionary<State, CustomerState> _states = new();

        public CustomerStateFactory(CustomerStateManager currentContext)
        {
            _states[State.Sleeping] = new SleepingOnBed(currentContext, this);
            _states[State.Waiting] = new WaitingInReceptionLine(currentContext, this);
            _states[State.WalkingToReceptionQueueLine] = new WalkingToReceptionQueueLine(currentContext, this);
            _states[State.WalkingToRoomBed] = new WalkingToRoomBed(currentContext, this);
            _states[State.WalkingToTaxi] = new WalkingToTaxi(currentContext, this);
        }

        public CustomerState Sleeping() => _states[State.Sleeping];
        public CustomerState Waiting() => _states[State.Waiting];
        public CustomerState WalkingToReceptionQueueLine() => _states[State.WalkingToReceptionQueueLine];
        public CustomerState WalkingToRoomBed() => _states[State.WalkingToRoomBed];
        public CustomerState WalkingToTaxi() => _states[State.WalkingToTaxi];

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
