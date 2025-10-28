using UnityEngine;
using UnityEngine.UI;

public class PauseScreenBehavior : MonoBehaviour
{
    private static PauseScreenBehavior instance;
    [SerializeField] private Image _universalControlsImage;
    [SerializeField] private Sprite _arcadeControlsSprite;
    [SerializeField] private Sprite _keyboardControlsSprite;
    [SerializeField] private GameObject _controlsFS;

    public Image UniversalControlsImage { get => _universalControlsImage; set => _universalControlsImage = value; }
    public static PauseScreenBehavior Instance { get => instance; set => instance = value; }
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        if (StaticData.IsKeyboardControls)
            _universalControlsImage.sprite = _keyboardControlsSprite;
        else
            _universalControlsImage.sprite = _arcadeControlsSprite;
    }
    public void OpenUniversalControls()
    {
        _universalControlsImage.gameObject.SetActive(true);
        GameManager.Instance.UniversalEventSystem.SetSelectedGameObject(_controlsFS);
    }
    public void CloseUniversalControls()
    {
        _universalControlsImage.gameObject.SetActive(false);
        GameManager.Instance.UniversalEventSystem.SetSelectedGameObject(GameManager.Instance.PauseFS);
    }
}
