using UnityEngine;
using UnityEngine.UI;

public class ActivePieceController : MonoBehaviour
{
    [SerializeField] private PlayerMatch3 _owner;
    [SerializeField] private MatchPieceSO _matchPiece;
    [SerializeField] private GridPoint _gridPoint;

    private Vector2 positionOnBoard;

    public GridPoint GridPoint { get => _gridPoint; set => _gridPoint = value; }
    public Vector2 PositionOnBoard { get => positionOnBoard; set => positionOnBoard = value; }

    public void SetUp(PlayerMatch3 owner, MatchPieceSO matchPiece, GridPoint gridPoint)
    {
        _owner = owner;
        _matchPiece = matchPiece;
        SetPosition(gridPoint);
        ApplySprite();
        SetUpInteractability();
    }
    
    public void GrabPiece()
    {
        //_owner.SelectedPieces.Push(this);
        //if (_owner.SelectedPieces.IsFull())
        //    _owner.PieceMover.SwapPieces();
        Debug.Log(_owner.PlayerName + "-\t grabs [" + _gridPoint.X + "," +  _gridPoint.Y + "]");
    }
    public void ReleasePiece()
    {
        Debug.Log("Releases\t\t[" + _gridPoint.X + "," + _gridPoint.Y + "]");
    }

    private void SetPosition(GridPoint gridPoint)
    {
        _gridPoint = gridPoint;
        ResetPositionOnBoard();
    }
    private void SetUpInteractability()
    {
        if (_matchPiece.BoardFunction != Enums.MatchPieceFunction.Unmoveable)
            return;
        GetComponent<Button>().enabled = false;
    }
    private void ApplySprite()
    {
        gameObject.GetComponent<Image>().sprite = _matchPiece.Sprite;
    }
    private void ResetPositionOnBoard()
    {
        positionOnBoard = new Vector2(_owner.HolderStartOffset.x + (_owner.PieceSize.x * _gridPoint.X), _owner.HolderStartOffset.y - (_owner.PieceSize.y * _gridPoint.Y));
    }
}
