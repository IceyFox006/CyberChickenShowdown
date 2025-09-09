using UnityEngine;

public class MatchPieceMovement : MonoBehaviour
{
    [SerializeField] private PlayerMatch3 _game;
    private ActivePieceController movingPiece;
    private GridPoint swapToPoint;
    private GridPoint swapFromPoint;

    private void Start()
    {
        
    }
}
