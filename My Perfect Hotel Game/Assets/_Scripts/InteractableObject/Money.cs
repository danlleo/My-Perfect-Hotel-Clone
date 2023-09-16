using System;
using UnityEngine;

namespace InteractableObject
{
    [SelectionBase]
    public class Money : Interactable
    {
        public override void Interact()
        {
            print("Interacting");
        }

        public override bool TryInteractWithCallback(out Action onComplete)
        {
            // TODO: Implement if AI will pick up the money for the player
            onComplete = null;
            return false;
        }
    }
}
