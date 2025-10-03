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
    [SerializeField] private MatchPieceSO _virusPiece;
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
    public MatchPieceSO VirusPiece { get => _virusPiece; set => _virusPiece = value; }

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
    public Player DetermineWinner()
    {
        //Player with the most HP
        if (_player1.CurrentHP > _player2.CurrentHP)
            return _player1;
        else if (_player1.CurrentHP < _player2.CurrentHP)
            return _player2;

        //Player with the most super
        if (_player1.CurrentSuper > _player2.CurrentSuper)
            return _player1;
        else if (_player1.CurrentSuper < _player2.CurrentSuper)
            return _player2;

        Debug.Log("Random winner.");
        //Random winner
        if (Random.Range(0, 2) == 0)
            return _player1;
        else
            return _player2;
    }
    public void EndGame(Player winner)
    {
        winner.Data.Wins++;
        StaticData.CurrentMatchCount++;
        Debug.Log(StaticData.CurrentMatchCount + "/" + StaticData.InitialMatchCount);
        if (StaticData.CurrentMatchCount > StaticData.InitialMatchCount)
        {
            winner.Data.IsWinner = true;
            SceneManager.LoadScene("WinLoseScene");
        }
        else
        {
            Debug.Log("NEW MATCH!");
            _player1.Data.SavedSuper = _player1.CurrentSuper;
            _player2.Data.SavedSuper = _player2.CurrentSuper;
            SceneManager.LoadScene("GameScreen");
        }
    }
}
