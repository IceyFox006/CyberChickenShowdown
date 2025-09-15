using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "ElementSO", menuName = "Scriptable Objects/ElementSO")]
public class ElementSO : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Enums.Element _element;
    [SerializeField] private Enums.Element[] _weaknesses;
    [SerializeField] private Enums.Element[] _resistances;

    public Enums.Element Element { get => _element; set => _element = value; }
    public string Name { get => _name; set => _name = value; }
    public Enums.Element[] Weaknesses { get => _weaknesses; set => _weaknesses = value; }
    public Enums.Element[] Resistances { get => _resistances; set => _resistances = value; }
}
