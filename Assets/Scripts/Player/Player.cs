using UnityEngine;
using UnityEngine.InputSystem.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerSO _data;
    [SerializeField] private PlayerGOController _gameObjectController;
    [SerializeField] private InputController _inputController;
    [SerializeField] private PlayerMatch3 _game;
    [SerializeField] private PlayerUIHandler _uiHandler;
    [SerializeField] private CombatManager _combatManager;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private MultiplayerEventSystem _eventSystem;

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
    public AudioManager AudioManager { get => _audioManager; set => _audioManager = value; }
    public MultiplayerEventSystem EventSystem { get => _eventSystem; set => _eventSystem = value; }
    public InputController InputController { get => _inputController; set => _inputController = value; }

    private void Awake()
    {
        _game.Owner = this;
        _inputController.Owner = this;
        
        currentHP = _data.Fighter.HP;
        currentSuper = _data.SavedSuper;
    }
    public float GetHPPercentage()
    {
        return currentHP / (float)_data.Fighter.HP;
    }
}
