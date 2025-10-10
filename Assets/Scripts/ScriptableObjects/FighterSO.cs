using UnityEngine;

[CreateAssetMenu(fileName = "FighterSO", menuName = "Scriptable Objects/FighterSO")]
public class FighterSO : ScriptableObject
{
    [Header("General")]
    [SerializeField] private string _name;
    [SerializeField] ElementSO _element;
    [TextArea(5, 1)][SerializeField] private string _superDescription;
    [TextArea(2, 1)][SerializeField] private string[] _winQuotes;

    [Header("Stats")]
    [SerializeField] private int _HP;
    [SerializeField] private int _Attack;
    [SerializeField][Range(0, 1)] private float _blockEffectiveness = 0.8f;
    [SerializeField] private Enums.SuperFunction _superFunction;
    [SerializeField] private int _superCapacity = 100;
    [SerializeField][Range(0, 1)] private float _superFillSpeed = 0.1f;
    [SerializeField] private float _superDrainRate = 1f;
    [SerializeField] private float _superEffectiveness = 10f;

    [Header("Game Visuals")]
    [SerializeField] private Sprite _gameScreenPortrait;
    [SerializeField] private Sprite _legUpIcon;
    [SerializeField] private RuntimeAnimatorController _animationController;
    [SerializeField] private Sprite _superVisual;
    [SerializeField] private Gradient _superGradient;
    [SerializeField] private Color _glowColor;

    [Header("Select Screen")]
    [SerializeField] private Sprite _portrait;


    public string Name { get => _name; set => _name = value; }
    public int HP { get => _HP; set => _HP = value; }
    public int Attack { get => _Attack; set => _Attack = value; }
    public int SuperCapacity { get => _superCapacity; set => _superCapacity = value; }
    public ElementSO Element { get => _element; set => _element = value; }
    public float SuperFillSpeed { get => _superFillSpeed; set => _superFillSpeed = value; }
    public float BlockEffectiveness { get => _blockEffectiveness; set => _blockEffectiveness = value; }
    public float SuperDrainRate { get => _superDrainRate; set => _superDrainRate = value; }
    public Enums.SuperFunction SuperFunction { get => _superFunction; set => _superFunction = value; }
    public float SuperEffectiveness { get => _superEffectiveness; set => _superEffectiveness = value; }
    public RuntimeAnimatorController AnimationController { get => _animationController; set => _animationController = value; }
    public string[] WinQuotes { get => _winQuotes; set => _winQuotes = value; }
    public Sprite SuperVisual { get => _superVisual; set => _superVisual = value; }
    public Sprite Portrait { get => _portrait; set => _portrait = value; }
    public string SuperDescription { get => _superDescription; set => _superDescription = value; }
    public Color GlowColor { get => _glowColor; set => _glowColor = value; }
    public Gradient SuperGradient { get => _superGradient; set => _superGradient = value; }
    public Sprite GameScreenPortrait { get => _gameScreenPortrait; set => _gameScreenPortrait = value; }
    public Sprite LegUpIcon { get => _legUpIcon; set => _legUpIcon = value; }
}
