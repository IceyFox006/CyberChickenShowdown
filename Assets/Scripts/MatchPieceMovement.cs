using UnityEngine;

public class MatchPieceMovement : MonoBehaviour
{
    private PlayerMatch3 game;
    private ActivePieceController movingPiece;
    private GridPoint destinationGridPoint;

    private void Awake()
    {
        game = GetComponent<PlayerMatch3>();
    }
    private void Update()
    {
        if (movingPiece != null)
        {
            //Vector2 moveDirection = (swapToPiece.transform.position - swapFromPiece.transform.position);
            //Vector2 normalizedDirection = moveDirection.normalized;
            //Vector2 absoluteDirection = new Vector2(Mathf.Abs(moveDirection.x), Mathf.Abs(moveDirection.y));

            //newIndex = GridPoint.PositionClone(movingPiece.GridPoint);
            //GridPoint add = GridPoint.zero;

            Vector2 position = game.GetPositionFromGridPoint(movingPiece.GridPoint);


        }
    }
    public void SwapPieces()
    {

    }
}
