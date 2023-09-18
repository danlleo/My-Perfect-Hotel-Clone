using System.Collections.Generic;
using InteractableObject;
using Misc;
using UnityEngine;
using Utilities;

public class InteractableObjectsStorage : Singleton<InteractableObjectsStorage>
{
    [Tooltip("Populate list with objects that will be possible to interact with")]
    [SerializeField] private List<Interactable> _interactableObjectList = new();
    
    public IEnumerable<Interactable> GetInteractableObjects() => _interactableObjectList;

    public void AddInteractableObject(Interactable interactable) => _interactableObjectList.Add(interactable);

    #region Validation

#if UNITY_EDITOR
    private void OnValidate()
    {
        EditorValidation.AreEnumerableValues(this, nameof(_interactableObjectList), _interactableObjectList);
    }
#endif

    #endregion
}