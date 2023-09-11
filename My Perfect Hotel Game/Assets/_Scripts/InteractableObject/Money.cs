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
    }
}
