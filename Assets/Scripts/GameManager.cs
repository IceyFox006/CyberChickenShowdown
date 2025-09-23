using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    [SerializeField] private float _tick;

    [Header("General")]
    [SerializeField] private Player _player1;
    [SerializeField] private Player _player2;
    [SerializeField] private int _gameTime = 60; //By ticks

    [Header("Match Game")]
    [SerializeField] private MatchPieceSO _wallPiece;
    [SerializeField] private MatchPieceSO _emptyPiece;
    [SerializeField] private MatchPieceSO[] _matchPieces = new MatchPieceSO[0];

    [Header("Combat")]
    [SerializeField] private float _STABMultiplier;
    [SerializeField] private float _comboAdditiveMultiplier;
    [SerializeField] private float _weaknessMultiplier; //1.5
    [SerializeField] private float _resistanceMultiplier; //0.5
    [SerializeField] private float _blockThreshold;
    [SerializeField] private float _blockDrainSpeed;

    public static GameManager Instance { get => instance; set => instance = value; }
    public MatchPieceSO WallPiece { get => _wallPiece; set => _wallPiece = value; }
    public MatchPieceSO EmptyPiece { get => _emptyPiece; set => _emptyPiece = value; }
    public MatchPieceSO[] MatchPieces { get => _matchPieces; set => _matchPieces = value; }
    public float STABMultiplier { get => _STABMultiplier; set => _STABMultiplier = value; }
    public float ComboAdditiveMultiplier { get => _comboAdditiveMultiplier; set => _comboAdditiveMultiplier = value; }
    public float WeaknessMultiplier { get => _weaknessMultiplier; set => _weaknessMultiplier = value; }
    public float ResistanceMultiplier { get => _resistanceMultiplier; set => _resistanceMultiplier = value; }
    public float BlockThreshold { get => _blockThreshold; set => _blockThreshold = value; }
    public float Tick { get => _tick; set => _tick = value; }
    public float BlockDrainSpeed { get => _blockDrainSpeed; set => _blockDrainSpeed = value; }
    public int GameTime { get => _gameTime; set => _gameTime = value; }

    private void Awake()
    {
        instance = this;
    }
    public Player GetOpponent(Player player)
    {
        if (player == _player1)
            return _player2;
        else if (player == _player2)
            return _player1;
        return null;
    }
    public void EndGame()
    {
        //Set winner & loser
        SceneManager.LoadScene("WinLoseScene");
    }
}
