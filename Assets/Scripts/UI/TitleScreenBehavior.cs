using UnityEngine;
using UnityEngine.EventSystems;

public class TitleScreenBehavior : MonoBehaviour
{
    private static TitleScreenBehavior instance;

    [SerializeField] private GameObject _tutorialButton;
    [SerializeField] private GameObject _tutorial;
    [SerializeField] private GameObject _keyboardControlsTutorial;
    [SerializeField] private GameObject _arcadeControlsTutorial;
    [SerializeField] private GameObject _exitTutorialButton;

    public static TitleScreenBehavior Instance { get => instance; set => instance = value; }

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
