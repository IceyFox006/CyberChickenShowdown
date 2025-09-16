using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "ElementSO", menuName = "Scriptable Objects/ElementSO")]
public class ElementSO : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Enums.Element _element;
    [SerializeField] private ElementSO[] _weaknesses;
    [SerializeField] private ElementSO[] _resistances;
    //[SerializeField] private Enums.Element[] _weaknesses;
    //[SerializeField] private Enums.Element[] _resistances;

    public Enums.Element Element { get => _element; set => _element = value; }
    public string Name { get => _name; set => _name = value; }
    public ElementSO[] Weaknesses { get => _weaknesses; set => _weaknesses = value; }
    public ElementSO[] Resistances { get => _resistances; set => _resistances = value; }
    //public Enums.Element[] Weaknesses { get => _weaknesses; set => _weaknesses = value; }
    //public Enums.Element[] Resistances { get => _resistances; set => _resistances = value; }
}
