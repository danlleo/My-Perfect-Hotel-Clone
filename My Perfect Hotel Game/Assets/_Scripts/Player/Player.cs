using System.Collections.Generic;
using Events;
using JetBrains.Annotations;
using TransportableObjects;
using UnityEngine;
using Utilities;

namespace Player
{
    [SelectionBase]
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(Interact))]
    [RequireComponent(typeof(Inventory))]
    [RequireComponent(typeof(AnimationsController))]
    [RequireComponent(typeof(PlayerWalkingStateChangedEvent))]
    [DisallowMultipleComponent]
    public class Player : MonoBehaviour
    {
        public const float RADIUS = .5f;
        public const float HEIGHT = 1f;
        
        [HideInInspector] public PlayerWalkingStateChangedEvent WalkingStateChangedEvent;
        private Inventory _inventory;

        private void Awake()
        {
            WalkingStateChangedEvent = GetComponent<PlayerWalkingStateChangedEvent>();
            _inventory = GetComponent<Inventory>();
        }

        public Inventory GetInventory() => _inventory;
    }
}
