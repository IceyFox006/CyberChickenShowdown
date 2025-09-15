using UnityEngine;

[CreateAssetMenu(fileName = "FighterSO", menuName = "Scriptable Objects/FighterSO")]
public class FighterSO : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] Enums.Element _element;
    [SerializeField] private int _HP;
    [SerializeField] private int _Attack;
    private int superCapacity = 100;

    public string Name { get => _name; set => _name = value; }
    public Enums.Element Element { get => _element; set => _element = value; }
    public int HP { get => _HP; set => _HP = value; }
    public int Attack { get => _Attack; set => _Attack = value; }
    public int SuperCapacity { get => superCapacity; set => superCapacity = value; }
}
