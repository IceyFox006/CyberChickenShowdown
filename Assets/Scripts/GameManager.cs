using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    [SerializeField] private float _tick;
    [SerializeField] private PlayerInput _universalInput;

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
    [SerializeField] private float _weaknessMultiplier; //1.15
    [SerializeField] private float _resistanceMultiplier; //0.85
    [SerializeField] private float _blockThreshold;
    [SerializeField] private float _blockDrainSpeed;
    [SerializeField] private float _legUpMultiplier;

    [Header("UI")]
    [SerializeField] private GameObject _pauseCanvas;
    private bool paused;

    private InputAction reset;
    private InputAction quit;

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
    public float LegUpMultiplier { get => _legUpMultiplier; set => _legUpMultiplier = value; }
    public GameObject PauseCanvas { get => _pauseCanvas; set => _pauseCanvas = value; }
    public bool Paused { get => paused; set => paused = value; }

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        reset = _universalInput.currentActionMap.FindAction("Reset");
        quit = _universalInput.currentActionMap.FindAction("Quit");

        reset.performed += Reset_performed;
        quit.performed += Quit_performed;
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

        //Random winner
        if (Random.Range(0, 2) == 0)
            return _player1;
        else
            return _player2;
    }
    public void EndRound(Player winner)
    {
        winner.Data.Wins++;
        StaticData.CurrentMatchCount++;
        if (StaticData.CurrentMatchCount > StaticData.InitialMatchCount)
            SceneManager.LoadScene("WinLoseScene");
        else
        {
            _player1.Data.SavedSuper = _player1.CurrentSuper;
            _player2.Data.SavedSuper = _player2.CurrentSuper;
            SceneManager.LoadScene("GameScreen");
        }
    }

    private void Reset_performed(InputAction.CallbackContext obj)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void Quit_performed(InputAction.CallbackContext obj)
    {
        if (GameManager.Instance.Paused)
            GameManager.Instance.ResumeGame();
        else
            GameManager.Instance.PauseGame();
        /*
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        */
    }

    public void PauseGame()
    {
        paused = true;
        PauseCanvas.SetActive(true);
        Time.timeScale = 0;
        Debug.Log("Pause");
    }

    public void ResumeGame()
    {
        paused = false;
        PauseCanvas.SetActive(false);
        Time.timeScale = 1;
        Debug.Log("Resume");
    }

    private void OnDestroy()
    {
        reset.performed -= Reset_performed;
        quit.performed -= Quit_performed;
    }
}
