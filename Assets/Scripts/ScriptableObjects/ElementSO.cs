using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "ElementSO", menuName = "Scriptable Objects/ElementSO")]
public class ElementSO : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Enums.Element _element;
    [SerializeField] private ElementSO[] _weaknesses;
    [SerializeField] private ElementSO[] _resistances;

    [SerializeField] private Sprite _icon;
    [SerializeField] private FloatingText _damageFT;

    public Enums.Element Element { get => _element; set => _element = value; }
    public string Name { get => _name; set => _name = value; }
    public ElementSO[] Weaknesses { get => _weaknesses; set => _weaknesses = value; }
    public ElementSO[] Resistances { get => _resistances; set => _resistances = value; }
    public Sprite Icon { get => _icon; set => _icon = value; }
    public FloatingText DamageFT { get => _damageFT; set => _damageFT = value; }
}
