/*
 * Marlow Greenan
 * 8/31/2025
 * 
 * A scriptable object for match pieces.
 */
using UnityEngine;

[CreateAssetMenu(fileName = "MatchPieceSO", menuName = "Scriptable Objects/MatchPieceSO")]
public class MatchPieceSO : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Enums.MatchPieceFunction _boardFunction;
    [SerializeField] private Enums.Element _element;
    [SerializeField] private Sprite _sprite;

    public string Name { get => _name; set => _name = value; }
    public Enums.MatchPieceFunction BoardFunction { get => _boardFunction; set => _boardFunction = value; }
    public Sprite Sprite { get => _sprite; set => _sprite = value; }
    public Enums.Element Element { get => _element; set => _element = value; }
}
