using System;
using UnityEngine;

namespace InteractableObject
{
    public abstract class Interactable : MonoBehaviour
    {
        public abstract void Interact();
        
        public abstract bool TryInteractWithCallback(out Action onComplete);
    }
}
