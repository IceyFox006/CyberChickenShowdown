using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private int _ID;
    [SerializeField] private PlayerSO _playerData;
    [SerializeField] private PlayerGOController _gameObjectController;
    [SerializeField] private InputController _inputController;
    [SerializeField] private PlayerMatch3 _game;
    [SerializeField] private PlayerUIHandler _uiHandler;
    [SerializeField] private CombatManager _combatManager;

    [Header("Combat")]
    [SerializeField] private FighterSO _fighter;
    private float currentHP;
    private float currentSuper = 0;

    public PlayerMatch3 Game { get => _game; set => _game = value; }
    public FighterSO Fighter { get => _fighter; set => _fighter = value; }
    public float CurrentHP { get => currentHP; set => currentHP = value; }
    public float CurrentSuper { get => currentSuper; set => currentSuper = value; }
    public PlayerUIHandler UiHandler { get => _uiHandler; set => _uiHandler = value; }
    public CombatManager CombatManager { get => _combatManager; set => _combatManager = value; }
    public PlayerGOController GameObjectController { get => _gameObjectController; set => _gameObjectController = value; }
    public PlayerSO PlayerData { get => _playerData; set => _playerData = value; }

    private void Awake()
    {
        _game.Owner = this;
        _inputController.Owner = this;
        
        if (_playerData.Fighter != null)
            _fighter = _playerData.Fighter;
        currentHP = _fighter.HP;
    }
}
