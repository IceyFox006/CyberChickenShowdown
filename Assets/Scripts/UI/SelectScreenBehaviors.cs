using System.Diagnostics.Tracing;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectScreenBehaviors : MonoBehaviour
{

    [SerializeField] private PlayerSO _player;
    private bool hasSelected = false;

    [SerializeField] private GameObject _lockedInVisual;

    [Header("Fighter Info")]
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _elementText;
    [SerializeField] private Image _elementImage;
    [SerializeField] private Image _portraitImage;

    public bool HasSelected { get => hasSelected; set => hasSelected = value; }
    public PlayerSO Player { get => _player; set => _player = value; }

    private void Awake()
    {
        if (_player.ID == 1)
            StaticData.Player1 = _player;
        else
            StaticData.Player2 = _player;
    }
    public SelectScreenBehaviors GetOtherPlayer()
    {
        SelectScreenBehaviors[] players = FindObjectsOfType<SelectScreenBehaviors>();
        if (players.Length > 2)
            throw new System.Exception("Too many players.");
        if (players[0].Player == _player)
            return players[1];
        else
            return players[0];
    }
    public bool HaveBothPlayersSelected()
    {
        if (HasSelected && GetOtherPlayer().HasSelected)
            return true;
        return false;
    }
    public void Button_SelectFighter(FighterSO fighter)
    {
        if (hasSelected)
            return;
        _player.Fighter = fighter;
        _lockedInVisual.SetActive(true);
        hasSelected = true;
        if (HaveBothPlayersSelected())
            SceneManager.LoadScene("GameScreen");
    }
    public void EnterHover_ShowFighterInformation(FighterSO fighter)
    {
        if (hasSelected) 
            return;
        _nameText.text = fighter.Name;
        _elementText.text = fighter.Element.Name;
        _elementImage.sprite = fighter.Element.Icon;
        _portraitImage.sprite = fighter.Portrait;
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
