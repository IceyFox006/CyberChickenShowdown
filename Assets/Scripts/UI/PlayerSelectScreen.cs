using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class PlayerSelectScreen : MonoBehaviour
{

    [SerializeField] private PlayerSO _player;
    private bool hasSelected = false;
    private FighterButtonSelect currentSelected;

    [SerializeField] private MultiplayerEventSystem _eventSystem;

    [SerializeField] private GameObject _lockedInVisual;

    [Header("Fighter Info")]
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _elementText;
    [SerializeField] private Image _elementImage;
    [SerializeField] private Image _portraitImage;
    [SerializeField] private TMP_Text _superDescriptionText;

    public bool HasSelected { get => hasSelected; set => hasSelected = value; }
    public PlayerSO Player { get => _player; set => _player = value; }
    public MultiplayerEventSystem EventSystem { get => _eventSystem; set => _eventSystem = value; }
    public FighterButtonSelect CurrentSelected { get => currentSelected; set => currentSelected = value; }

    private void Awake()
    {
        if (_player.ID == 1)
            StaticData.Player1 = _player;
        else
            StaticData.Player2 = _player;
    }
    public void Button_SelectFighter(FighterSO fighter)
    {
        if (hasSelected)
            return;
        _eventSystem.sendNavigationEvents = false;
        _player.Fighter = fighter;
        _lockedInVisual.SetActive(true);
        AudioManager.Instance.PlaySound("LockInFighter");
    }
    public void Button_SelectRandomFighter()
    {
        int random = Random.Range(0, SelectScreenBehavior.Instance.Fighters.Length);
        Button_SelectFighter(SelectScreenBehavior.Instance.Fighters[random]);
    }

    [HideInInspector]
    public void UnselectFighter()
    {
        _eventSystem.sendNavigationEvents = true;
        _player.Fighter = null;
        _lockedInVisual.SetActive(false);
        hasSelected = false;
        if (currentSelected != null)
            currentSelected.Exit();
        currentSelected = null;
    }
    public void EnterHover_ShowFighterInformation(FighterSO fighter)
    {
        if (hasSelected) 
            return;
        _nameText.text = fighter.Name;
        _elementText.text = fighter.Element.Name;
        _elementImage.sprite = fighter.Element.Icon;
        _portraitImage.sprite = fighter.Portrait;
        _superDescriptionText.text = fighter.SuperDescription;
    }
    public void Button_HowToPlay()
    {
        Debug.Log("Not implemented.");
    }
    public void Button_Return()
    {
        Debug.Log("Not implemented.");
    }
}
