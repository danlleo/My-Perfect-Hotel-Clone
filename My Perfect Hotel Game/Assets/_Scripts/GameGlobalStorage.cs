using Misc;
using UnityEngine;
using Utilities;

public class GameGlobalStorage : Singleton<GameGlobalStorage>
{
    [SerializeField] private Player.Player _player;

    public Player.Player GetPlayer() => _player;
    
    #region Validation

#if UNITY_EDITOR
    private void OnValidate()
    {
        EditorValidation.IsNullValue(this, nameof(_player), _player);
    }
#endif

    #endregion
}
