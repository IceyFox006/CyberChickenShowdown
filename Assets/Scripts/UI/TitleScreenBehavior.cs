using UnityEngine;
using UnityEngine.EventSystems;

public class TitleScreenBehavior : MonoBehaviour
{
    private static TitleScreenBehavior instance;

    [SerializeField] private PlayerSO _player1;
    [SerializeField] private PlayerSO _player2;
    [SerializeField] private FighterSO _testDummy;

    [SerializeField] private GameObject _tutorialButton;
    [SerializeField] private GameObject _tutorial;
    [SerializeField] private GameObject _keyboardControlsTutorial;
    [SerializeField] private GameObject _arcadeControlsTutorial;
    [SerializeField] private GameObject _exitTutorialButton;

    public static TitleScreenBehavior Instance { get => instance; set => instance = value; }
    public PlayerSO Player1 { get => _player1; set => _player1 = value; }
    public PlayerSO Player2 { get => _player2; set => _player2 = value; }
    public FighterSO TestDummy { get => _testDummy; set => _testDummy = value; }

    public void OpenTutorial()
    {
        CloseTutorial();
        _tutorial.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_exitTutorialButton);
        if (StaticData.IsKeyboardControls)
            _keyboardControlsTutorial.SetActive(true);

        else
            _arcadeControlsTutorial.SetActive(true);
    }
    public void CloseTutorial()
    {
        _arcadeControlsTutorial.SetActive(false);
        _keyboardControlsTutorial.SetActive(false);
        _tutorial.SetActive(false);
        EventSystem.current.SetSelectedGameObject(_tutorialButton);
    }
}
