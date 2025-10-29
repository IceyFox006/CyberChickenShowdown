using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSO", menuName = "Scriptable Objects/CharacterSO")]
public class CharacterSO : ScriptableObject
{
    [SerializeField] private string _name;

    public string Name { get => _name; set => _name = value; }

    public enum Expression
    {
        Default,
    }
}
