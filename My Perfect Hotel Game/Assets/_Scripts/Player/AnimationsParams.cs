using UnityEngine;

namespace Player
{
    public static class AnimationsParams
    {
        public static readonly int IsWalking = Animator.StringToHash(nameof(IsWalking));
        public static readonly int IsCarrying = Animator.StringToHash(nameof(IsCarrying));
    }
}
