using System;
using UnityEngine;
using Utilities;

[RequireComponent(typeof(TransformsTogglerAnimator))]
public class TransformsToggler : MonoBehaviour
{
    [SerializeField] private Transform[] _transformsToToggle;
    private TransformsTogglerAnimator _togglerAnimator;

    private void Awake()
    {
        _togglerAnimator = GetComponent<TransformsTogglerAnimator>();
    }

    private void OnEnable() => SetActive(false);

    public void SetActive(bool isActive)
    {
        foreach (Transform objectToToggle in _transformsToToggle)
            if (objectToToggle != null)
            {
                if (isActive) 
                    _togglerAnimator.Enable(objectToToggle, () => objectToToggle.gameObject.SetActive(true));
                else 
                    _togglerAnimator.Disable(objectToToggle,() => objectToToggle.gameObject.SetActive(false));
            }
    }

    #region Validation

#if UNITY_EDITOR
    private void OnValidate()
    {
        EditorValidation.AreEnumerableValues(this, nameof(_transformsToToggle), _transformsToToggle);
        EditorValidation.IsPositiveValue(this, nameof(_transformsToToggle), _transformsToToggle.Length, false);
    }
#endif

    #endregion
}
