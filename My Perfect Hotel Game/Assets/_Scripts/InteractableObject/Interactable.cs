using System;
using UnityEngine;

namespace InteractableObject
{
    public abstract class Interactable : MonoBehaviour
    {
        public abstract void Interact();
        
        /// <summary>
        /// This method will be used by AI.
        /// Once the AI interacts with the object, onComplete action will be returned with a bool value of true,
        /// that way it would be easy to track when the AI finished it's action.
        /// </summary>
        /// 
        public abstract bool TryInteractWithCallback(out Action onComplete);
    }
}
