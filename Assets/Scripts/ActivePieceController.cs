using UnityEngine;
using UnityEngine.UI;

public class ActivePieceController : MonoBehaviour
{
    [SerializeField] private PlayerMatch3 _owner;
    [SerializeField] private MatchPieceSO _matchPiece;
    [SerializeField] private GridPoint _gridPoint;

    private Vector2 positionOnBoard;

    public void SetUp(PlayerMatch3 owner, MatchPieceSO matchPiece, GridPoint gridPoint)
    {
        _owner = owner;
        _matchPiece = matchPiece;
        SetPosition(gridPoint);
        ApplySprite();
    }
    public void SetPosition(GridPoint gridPoint)
    {
        _gridPoint = gridPoint;
        ResetPositionOnBoard();
    }

    public void ApplySprite()
    {
        gameObject.GetComponent<Image>().sprite = _matchPiece.Sprite;
    }
    public void ResetPositionOnBoard()
    {
        positionOnBoard = new Vector2(_owner.HolderStartOffset.x + (_owner.PieceSize.x * _gridPoint.X), _owner.HolderStartOffset.y - (_owner.PieceSize.y * _gridPoint.Y));
    }
}
