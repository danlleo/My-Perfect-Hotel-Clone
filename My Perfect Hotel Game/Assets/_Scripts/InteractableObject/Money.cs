using UnityEngine;

namespace InteractableObject
{
    [SelectionBase]
    [DisallowMultipleComponent]
    public class Money : Interactable
    {
        public override void Interact()
        {
            print("Interacting");
        }
    }
}
