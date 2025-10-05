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
    [SerializeField] private ElementSO _element;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private GameObject _breakParticlePrefab;

    public string Name { get => _name; set => _name = value; }
    public Enums.MatchPieceFunction BoardFunction { get => _boardFunction; set => _boardFunction = value; }
    public Sprite Sprite { get => _sprite; set => _sprite = value; }
    public ElementSO Element { get => _element; set => _element = value; }
    public GameObject BreakParticlePrefab { get => _breakParticlePrefab; set => _breakParticlePrefab = value; }
}
