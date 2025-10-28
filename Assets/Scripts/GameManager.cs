using UnityEngine;
using UnityEngine.EventSystems;
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
    private bool isTimerGoing = true;

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
    [SerializeField] private EventSystem _universalEventSystem;
    [SerializeField] private GameObject _pauseCanvas;
    [SerializeField] private GameObject _pauseFS;
    private bool paused;
    private CanvasGroup[] canvases;

    [Header("Camera")]
    [SerializeField] private CameraAnimator _cameraAnimator;

    private InputAction reset;
    private InputAction quit;

    #region GetSetters
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
    public CameraAnimator CameraAnimator { get => _cameraAnimator; set => _cameraAnimator = value; }
    public bool IsTimerGoing { get => isTimerGoing; set => isTimerGoing = value; }
    public EventSystem UniversalEventSystem { get => _universalEventSystem; set => _universalEventSystem = value; }
    public GameObject PauseFS { get => _pauseFS; set => _pauseFS = value; }
    #endregion

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

        canvases = FindObjectsByType<CanvasGroup>(FindObjectsSortMode.None);
    }
    public Player GetOpponent(Player player)
    {
        if (player == _player1)
            return _player2;
        else if (player == _player2)
            return _player1;
        return null;
    }
    public Player GetPlayerFromID(int id)
    {
        if (id == 1)
            return _player1;
        else if (id == 2)
            return _player2;
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
    public void EnableAllInput()
    {
        _player1.InputController.EnableInput();
        _player2.InputController.EnableInput();
        foreach (CanvasGroup canvasGroup in canvases)
            canvasGroup.interactable = true;
    }
    public void DisableAllInput()
    {
        _player1.InputController.DisableInput();
        _player2.InputController.DisableInput();
        foreach (CanvasGroup canvasGroup in canvases)
            canvasGroup.interactable = false;
    }
    public void ShowUI()
    {
        foreach (CanvasGroup canvasGroup in canvases)
            canvasGroup.alpha = 1;
    }
    public void HideUI()
    {
        foreach (CanvasGroup canvasGroup in canvases)
            canvasGroup.alpha = 0;
    }
    public void PlayCloseTransition(Player winner)
    {
        EndRound(winner);
        TransitionBehavior.Instance.PlayClose();
    }
    public void PlayCloseTransition(string scene)
    {
        TransitionBehavior.Instance.Events.SceneChange = scene;
        TransitionBehavior.Instance.PlayClose();
    }
    public void EndRound(Player winner)
    {
        winner.Data.Wins++;
        StaticData.CurrentMatchCount++;
    }
    public void NextRound()
    {
        if (StaticData.CurrentMatchCount > StaticData.InitialMatchCount)
            TransitionBehavior.Instance.PlayClose("WinLoseScene");//SceneManager.LoadScene("WinLoseScene");
        else
        {
            _player1.Data.SavedSuper = _player1.CurrentSuper;
            _player2.Data.SavedSuper = _player2.CurrentSuper;
            TransitionBehavior.Instance.PlayClose("GameScreen");//SceneManager.LoadScene("GameScreen");
        }
    }

    private void Reset_performed(InputAction.CallbackContext obj)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void Quit_performed(InputAction.CallbackContext obj)
    {
        if (paused)
        {
            PauseScreenBehavior.Instance.UniversalControlsImage.gameObject.SetActive(false);
            ResumeGame();
        }
        else
            PauseGame();
    }

    public void PauseGame()
    {
        if (!StaticData.IsKeyboardControls)
            return;
        paused = true;
        PauseCanvas.SetActive(true);
        _universalEventSystem.SetSelectedGameObject(_pauseFS);
        Time.timeScale = 0;
        HideUI();
        _player1.EventSystem.enabled = false;
        _player2.EventSystem.enabled = false;
    }
    
    public void ResumeGame()
    {
        if (!StaticData.IsKeyboardControls)
            return;
        paused = false;
        PauseCanvas.SetActive(false);
        Time.timeScale = 1;
        ShowUI();
        _player1.EventSystem.enabled = true;
        _player2.EventSystem.enabled = true;
    }

    private void OnDestroy()
    {
        reset.performed -= Reset_performed;
        quit.performed -= Quit_performed;
    }
}
