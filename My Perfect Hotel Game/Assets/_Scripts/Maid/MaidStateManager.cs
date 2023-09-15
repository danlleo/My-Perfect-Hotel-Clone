using System;
using Maid.States;
using UnityEngine;

namespace Maid
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Maid))]
    public class MaidStateManager : MonoBehaviour
    {
        public Maid CurrentMaid { get; private set; }

        private MaidState _currentState;

        public AwaitingState Awaiting;
        public MovingState Moving;

        private void Awake()
        {
            CurrentMaid = GetComponent<Maid>();
        }

        private void Start()
        {
            SetDefaultState(Awaiting);
        }
        
        private void SetDefaultState(MaidState targetState)
            => _currentState = targetState;
    }
}