using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private InputController _inputController;
    [SerializeField] private PlayerMatch3 _game;

    [Header("Combat")]
    [SerializeField] private FighterSO _fighter;
    private float currentHP;
    private float currentSuper = 0;

    public PlayerMatch3 Game { get => _game; set => _game = value; }
    public string Name { get => _name; set => _name = value; }
    public FighterSO Fighter { get => _fighter; set => _fighter = value; }
    public float CurrentHP { get => currentHP; set => currentHP = value; }
    public float CurrentSuper { get => currentSuper; set => currentSuper = value; }

    private void Awake()
    {
        _game.Owner = this;
        _inputController.Owner = this;
        
        currentHP = _fighter.HP;
    }
}
