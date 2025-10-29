using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSO", menuName = "Scriptable Objects/CharacterSO")]
public class CharacterSO : ScriptableObject
{
    [SerializeField] private string _name;

    [SerializeField] private int reputationPoints;
    public string Name { get => _name; set => _name = value; }
    public int ReputationPoints { get => reputationPoints; set => reputationPoints = value; }

    public enum Expression
    {
        Default,
    }
}
