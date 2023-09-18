using System;
using Events;
using UnityEngine;

namespace Player
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Player))]
    [RequireComponent(typeof(Animator))]
    public class AnimationsController : MonoBehaviour
    {
        private Player _player;
        private Animator _animator;
        private Movement _movement;
        private Inventory _inventory;

        private void Awake()
        {
            _player = GetComponent<Player>();
            _animator = GetComponent<Animator>();
            _movement = GetComponent<Movement>();
            _inventory = GetComponent<Inventory>();
        }
        
        private void OnEnable()
        {
            _player.WalkingStateChangedEvent.Event += Player_OnWalkingStateChangedEvent;
            _player.PickedAnObjectEvent.Event += Player_OnPickedOrDroppedAnObject;
            _player.DroppedAnObjectEvent.Event += Player_OnPickedOrDroppedAnObject;
        }

        private void OnDisable()
        {
            _player.WalkingStateChangedEvent.Event -= Player_OnWalkingStateChangedEvent;
            _player.PickedAnObjectEvent.Event -= Player_OnPickedOrDroppedAnObject;
            _player.DroppedAnObjectEvent.Event -= Player_OnPickedOrDroppedAnObject;
        }
        
        private void Player_OnWalkingStateChangedEvent(object sender, PlayerWalkingStateChangedEventArgs e) => 
            _animator.SetBool(AnimationsParams.IsWalking, e.IsWalking);
        
        private void Player_OnPickedOrDroppedAnObject(object sender, EventArgs e) => 
            _animator.SetBool(AnimationsParams.IsCarrying, _inventory.IsCarrying());
    }
}
