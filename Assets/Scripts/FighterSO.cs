using UnityEngine;

[CreateAssetMenu(fileName = "FighterSO", menuName = "Scriptable Objects/FighterSO")]
public class FighterSO : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] ElementSO _element;
    [SerializeField] private int _HP;
    [SerializeField] private int _Attack;
    [SerializeField] private int _superCapacity = 100;
    [SerializeField] [Range(0, 1)] private float _superFillSpeed = 0.1f;
    [SerializeField][Range(0, 1)] private float _blockEffectiveness = 0.8f;

    public string Name { get => _name; set => _name = value; }
    public int HP { get => _HP; set => _HP = value; }
    public int Attack { get => _Attack; set => _Attack = value; }
    public int SuperCapacity { get => _superCapacity; set => _superCapacity = value; }
    public ElementSO Element { get => _element; set => _element = value; }
    public float SuperFillSpeed { get => _superFillSpeed; set => _superFillSpeed = value; }
    public float BlockEffectiveness { get => _blockEffectiveness; set => _blockEffectiveness = value; }
}
