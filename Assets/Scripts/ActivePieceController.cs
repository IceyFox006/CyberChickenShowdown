using UnityEngine;
using UnityEngine.UI;

public class ActivePieceController : MonoBehaviour
{
    [SerializeField] private MatchPieceSO _matchPiece;
    [SerializeField] private GridPoint _gridPoint;

    private Vector2 positionOnBoard;

    public void SetUp(MatchPieceSO matchPiece, GridPoint gridPoint)
    {
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
        //positionOnBoard = new Vector2(GameController.Instance.TopLeftPiecePosition.x + (GameController.Instance.MatchPieceSize.x * _gridPoint.X), -GameController.Instance.TopLeftPiecePosition.y - (GameController.Instance.MatchPieceSize.y * _gridPoint.Y));
    }
}
