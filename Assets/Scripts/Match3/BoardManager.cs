using UnityEngine;

public class BoardManager : MonoBehaviour
{
    private static BoardManager instance;
    private int gameIndex = -1;

    [SerializeField] private BoardSO[] _boards;

    public static BoardManager Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        instance = this;
    }
    public Inspector2DArrayLayout GetRandomBoard()
    {
        if (gameIndex >= 0)
            return _boards[gameIndex].GameBoardLayout;
        gameIndex = Random.Range(0, _boards.Length);
        return _boards[gameIndex].GameBoardLayout;
    }
}
