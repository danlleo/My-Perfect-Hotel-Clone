using UnityEngine;
using UnityEngine.Serialization;

namespace TransportableObjects
{
    [CreateAssetMenu(fileName = "TransportableObject_", menuName = "Scriptable Objects/TransportableObjects/TransportableObject")]
    public class TransportableObjectSO : ScriptableObject
    {
        [FormerlySerializedAs("_transportableObjectPrefab")]
        [Space(10)]
        [Header("Transportable Object")]
        [Space(5)]

        #region VALUES THAT ARE ASSIGNED IN THE INSPECTOR
        
        [Tooltip("Populate with a prefab of type 'Transportable' that will represent transportable object")]
        [SerializeField] private Transportable _prefab;
        
        [Tooltip("Limit the amount of how much of this objects player can carry")]
        [SerializeField] [Min(1)] private int _maxCarryingAmount;

        [Tooltip("Populate with the object type")]
        [SerializeField] private Enums.TransportableObjectType _type;

        #endregion

        #region REFERENCE VALUES

        public Transportable Prefab => _prefab;
        public int MaxCarryingAmount => _maxCarryingAmount;
        public Enums.TransportableObjectType Type => _type;

        #endregion
    }
}
