using UnityEngine;
using UnityEngine.EventSystems;

public class SelectScreenBehavior : MonoBehaviour
{
    private static SelectScreenBehavior instance;

    [Header("Players")]
    [SerializeField] private EventSystem _universalEventSystem;
    [SerializeField] private PlayerSelectScreen _player1;
    [SerializeField] private PlayerSelectScreen _player2;
    [SerializeField] private FighterSO[] _fighters;

    [Header("UI")]
    [SerializeField] private GameObject _chooseMatchNumberUIGO;
    [SerializeField] private GameObject _fsCMNUI;
    [SerializeField] private Animator _roundsTransitionAnimator;

    public static SelectScreenBehavior Instance { get => instance; set => instance = value; }
    public GameObject ChooseMatchNumberUIGO { get => _chooseMatchNumberUIGO; set => _chooseMatchNumberUIGO = value; }
    public GameObject FsCMNUI { get => _fsCMNUI; set => _fsCMNUI = value; }
    public EventSystem UniversalEventSystem { get => _universalEventSystem; set => _universalEventSystem = value; }
    public FighterSO[] Fighters { get => _fighters; set => _fighters = value; }
    public PlayerSelectScreen Player1 { get => _player1; set => _player1 = value; }
    public PlayerSelectScreen Player2 { get => _player2; set => _player2 = value; }
    public Animator RoundsTransitionAnimator { get => _roundsTransitionAnimator; set => _roundsTransitionAnimator = value; }

    private void Awake()
    {
        instance = this;
    }
    public void SetLivesCount(int lives)
    {
        StaticData.LivesCount = lives;
        StaticData.Player1.Lives = lives;
        StaticData.Player2.Lives = lives;
        TransitionBehavior.Instance.PlayClose("GameScreen");
    }
    public void SetRoundCount(int  matchCount)
    {
        StaticData.InitialMatchCount = matchCount;
        StaticData.CurrentMatchCount = 1;

        TransitionBehavior.Instance.PlayClose("GameScreen");
    }
    public PlayerSelectScreen GetOtherPlayer(PlayerSelectScreen player)
    {
        if (player == _player1)
            return _player2;
        return _player1;
    }
    public bool HaveBothPlayersSelected()
    {
        return (_player1.HasSelected && _player2.HasSelected);
    }
}
