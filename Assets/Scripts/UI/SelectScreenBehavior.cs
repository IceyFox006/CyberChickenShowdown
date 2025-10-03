using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;

public class SelectScreenBehavior : MonoBehaviour
{
    private static SelectScreenBehavior instance;

    [Header("Players")]
    [SerializeField] private PlayerSelectScreen _player1;
    [SerializeField] private PlayerSelectScreen _player2;

    [Header("UI")]
    [SerializeField] private GameObject _chooseMatchNumberUIGO;

    public static SelectScreenBehavior Instance { get => instance; set => instance = value; }
    public GameObject ChooseMatchNumberUIGO { get => _chooseMatchNumberUIGO; set => _chooseMatchNumberUIGO = value; }

    public void Button_SetMatchCount(int  matchCount)
    {
        StaticData.InitialMatchCount = matchCount;
        StaticData.CurrentMatchCount = 1;

        SceneManager.LoadScene("GameScreen");
    }
    private void Awake()
    {
        instance = this;
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
