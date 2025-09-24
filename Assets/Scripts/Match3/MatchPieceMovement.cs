using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MatchPieceMovement : MonoBehaviour
{
    private PlayerMatch3 ownersGame;
    private ActivePieceController movingPiece;
    private GridPoint newGridPoint;
    private Vector2 moveToSpot;
    private Vector2 moveFromSpot;

    [SerializeField] private Sprite _currentlySelectedBorderSprite;
    [SerializeField] private Sprite _previouslySelectedBorderSprite;

    public Sprite CurrentlySelectedBorderSprite { get => _currentlySelectedBorderSprite; set => _currentlySelectedBorderSprite = value; }
    public Sprite PreviouslySelectedBorderSprite { get => _previouslySelectedBorderSprite; set => _previouslySelectedBorderSprite = value; }
    public Vector2 MoveToSpot { get => moveToSpot; set => moveToSpot = value; }
    public Vector2 MoveFromSpot { get => moveFromSpot; set => moveFromSpot = value; }
    public ActivePieceController MovingPiece { get => movingPiece; set => movingPiece = value; }

    private void Start()
    {
        ownersGame = GetComponent<PlayerMatch3>();
    }
    private void Update()
    {
        if (movingPiece != null)
        {
            Vector2 direction = moveToSpot - moveFromSpot;
            Vector2 normalizedDirection = direction.normalized;
            Vector2 absoluteDirection = new Vector2(Mathf.Abs(direction.x), Mathf.Abs(direction.y));

            newGridPoint = GridPoint.PositionClone(movingPiece.GridPoint);
            GridPoint addedPoint = GridPoint.zero;
            if (direction.magnitude > ownersGame.HolderStartOffset.x)
            {
                if (absoluteDirection.x > absoluteDirection.y)
                    addedPoint = new GridPoint((normalizedDirection.x > 0) ? 1 : -1, 0);
                else if (absoluteDirection.y > absoluteDirection.x)
                    addedPoint = new GridPoint(0, (normalizedDirection.y > 0) ? -1 : 1);
            }
            newGridPoint.Add(addedPoint);

            Vector2 position = ownersGame.GetPositionFromGridPoint(movingPiece.GridPoint);
            if (!newGridPoint.Equals(movingPiece.GridPoint))
                position += GridPoint.Multiply(new GridPoint(addedPoint.X, -addedPoint.Y), (int)(ownersGame.PieceSize.x / 2)).ToVector2();
            movingPiece.MovePositionTo(position);
        }
    }
    public void MovePiece(ActivePieceController piece)
    {
        if (movingPiece != null)
            return;
        moveFromSpot = piece.transform.position;
        movingPiece = piece;

    }
    public void DropPiece(bool swapping = false)
    {
        if (movingPiece == null)
            return;
        //movingPiece.transform.position = moveFromSpot; //!!!!
        if (!newGridPoint.Equals(movingPiece.GridPoint) && swapping)
            ownersGame.SwapPieces(true);//(movingPiece.GridPoint, newGridPoint);
        else
        {
            ownersGame.ResetPiece(movingPiece);
            ownersGame.EventSystem.SetSelectedGameObject(movingPiece.gameObject);
        }
        movingPiece.SelectedBorder.enabled = true;
        movingPiece = null;
        ownersGame.StartSwapPiece = null;
    }
}
