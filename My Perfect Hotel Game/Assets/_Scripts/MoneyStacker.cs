using InteractableObject;
using UnityEngine;
using Utilities;

public class MoneyStacker : MonoBehaviour
{
    [SerializeField] private Vector3Int _size;
    [SerializeField] private float _moneyBillGapY;
    [SerializeField] private Transform _moneyContainerTransform;
    [SerializeField] private Money _moneyBillPrefab;
    private Vector3 _moneyBillVisualSize;
    private Vector3 _radius;
    
    private void Start()
    {
        _moneyBillVisualSize = _moneyBillPrefab.GetMoneyVisualSize();
        _radius = new Vector3(
            _moneyBillVisualSize.x * (_size.x - 1) * 0.5f,
            _moneyBillVisualSize.y * (_size.y - 1) * 0.5f,
            _moneyBillVisualSize.z * (_size.z - 1) * 0.5f
        );
        
        StackMoney();
    }
    
    private void StackMoney()
    {
        for (var y = 0; y < _size.y; y++)
        for (var x = 0; x < _size.x; x++)
        for (var z = 0; z < _size.z; z++)
        {
            // Calculate the position for each money bill
            Vector3 spawnPosition = new(
                _moneyBillVisualSize.x * x - _radius.x,
                (_moneyBillVisualSize.y + _moneyBillGapY) * y,
                _moneyBillVisualSize.z * z - _radius.z
            );

            // Instantiate a money bill at the calculated position
            Money moneyBill = Instantiate(_moneyBillPrefab, _moneyContainerTransform);
            moneyBill.transform.localPosition = spawnPosition;
            
            InteractManager.Instance.AddInteractableObject(moneyBill);
        }
    }

    #region Validation

#if UNITY_EDITOR
    private void OnValidate()
    {
        EditorValidation.IsPositiveValue(this, nameof(_moneyBillGapY), _moneyBillGapY);
        EditorValidation.IsNullValue(this, nameof(_moneyContainerTransform), _moneyContainerTransform);
        EditorValidation.IsNullValue(this, nameof(_moneyBillPrefab), _moneyBillPrefab);
    }
#endif

    #endregion
}