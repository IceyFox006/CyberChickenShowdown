using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerSO _data;
    [SerializeField] private PlayerGOController _gameObjectController;
    [SerializeField] private InputController _inputController;
    [SerializeField] private PlayerMatch3 _game;
    [SerializeField] private PlayerUIHandler _uiHandler;
    [SerializeField] private CombatManager _combatManager;

    [Header("Combat")]
    private float currentHP;
    private float currentSuper = 0;

    public PlayerMatch3 Game { get => _game; set => _game = value; }
    public float CurrentHP { get => currentHP; set => currentHP = value; }
    public float CurrentSuper { get => currentSuper; set => currentSuper = value; }
    public PlayerUIHandler UiHandler { get => _uiHandler; set => _uiHandler = value; }
    public CombatManager CombatManager { get => _combatManager; set => _combatManager = value; }
    public PlayerGOController GameObjectController { get => _gameObjectController; set => _gameObjectController = value; }
    public PlayerSO Data { get => _data; set => _data = value; }

    private void Awake()
    {
        _game.Owner = this;
        _inputController.Owner = this;
        
        currentHP = _data.Fighter.HP;
        currentSuper = _data.SavedSuper;
    }
}
