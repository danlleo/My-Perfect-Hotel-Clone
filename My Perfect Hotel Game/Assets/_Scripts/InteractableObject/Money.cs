using System;
using UnityEngine;

namespace InteractableObject
{
    [SelectionBase] public class Money : Interactable
    {
        [SerializeField] private Vector3 _moneyBillSize = new(0.36f, 0.2f, 0.72f);

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

        public Vector3 GetMoneyVisualSize() => _moneyBillSize;
    }
}
