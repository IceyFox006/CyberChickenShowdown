using System.Collections.Generic;
using UnityEngine;

public class MatchPieceMovement : MonoBehaviour
{
    private PlayerMatch3 owner;
    private ActivePieceController movingPiece;
    private GridPoint destinationGridPoint;

    private ActivePieceController currentSelectedPiece;
    private ActivePieceController previousSelectedPiece;

    [SerializeField] private Sprite _currentlySelectedBorderSprite;
    [SerializeField] private Sprite _previouslySelectedBorderSprite;

    public ActivePieceController CurrentSelectedPiece { get => currentSelectedPiece; set => currentSelectedPiece = value; }
    public ActivePieceController PreviousSelectedPiece { get => previousSelectedPiece; set => previousSelectedPiece = value; }
    public Sprite CurrentlySelectedBorderSprite { get => _currentlySelectedBorderSprite; set => _currentlySelectedBorderSprite = value; }
    public Sprite PreviouslySelectedBorderSprite { get => _previouslySelectedBorderSprite; set => _previouslySelectedBorderSprite = value; }

    private void Awake()
    {
        owner = GetComponent<PlayerMatch3>();
    }
    private void Update()
    {
        if (movingPiece != null)
        {
            Vector2 moveDirection = (currentSelectedPiece.transform.position - previousSelectedPiece.transform.position);
            Vector2 normalizedDirection = moveDirection.normalized;
            Vector2 absoluteDirection = new Vector2(Mathf.Abs(moveDirection.x), Mathf.Abs(moveDirection.y));

            destinationGridPoint = GridPoint.PositionClone(movingPiece.GridPoint);
            GridPoint addedGridPoint = GridPoint.zero;

            //If the current selected piece is 40 or more pixels away from the previous selected piece.
            if (moveDirection.magnitude > owner.PieceSize.x)
            {
                if (absoluteDirection.x > absoluteDirection.y)
                    addedGridPoint = new GridPoint((normalizedDirection.x > 0) ? 1 : -1, 0);
                else if (absoluteDirection.y > absoluteDirection.x)
                    addedGridPoint = new GridPoint(0, (normalizedDirection.y > 0) ? 1 : -1);
            }
            destinationGridPoint.Add(addedGridPoint);

            Vector2 position = owner.GetPositionFromGridPoint(movingPiece.GridPoint);
            if (!destinationGridPoint.Equals(movingPiece.GridPoint))
                position += GridPoint.Multiply(new GridPoint(addedGridPoint.X, addedGridPoint.Y), (int)owner.HolderStartOffset.x).ToVector2();
            movingPiece.MovePiecePositionTo(position);

        }
    }
    private void MovePiece(ActivePieceController piece)
    {
        //Prevents moving if there is alread a piece moving.
        if (movingPiece != null)
            return;
        //!!!!!

    }
    private void DropPiece()
    {
        if (movingPiece == null)
            return;
        Debug.Log("Dropped");
        movingPiece = null;
    }
}
