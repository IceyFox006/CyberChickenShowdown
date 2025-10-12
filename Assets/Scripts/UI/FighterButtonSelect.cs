using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FighterButtonSelect : MonoBehaviour
{
    [SerializeField] private PlayerSelectScreen _owner;
    [SerializeField] private Button _button;
    [SerializeField] private Image _selectionImage;
    [SerializeField] private Image _hoverImage;

    private bool selected = false;
    private GameObject playerIndicator;

    public void Hover()
    {
        if (selected || _owner.HasSelected)
            return;
        Exit();
        _hoverImage.enabled = true;
        SpawnPlayerIndicator();
    }
    public void Select()
    {
        if (selected || _owner.HasSelected)
            return;
        Exit();
        _selectionImage.enabled = true;
        _owner.HasSelected = true;
        selected = true;
        if (SelectScreenBehavior.Instance.HaveBothPlayersSelected())
        {
            SelectScreenBehavior.Instance.ChooseMatchNumberUIGO.SetActive(true);
            SelectScreenBehavior.Instance.UniversalEventSystem.SetSelectedGameObject(SelectScreenBehavior.Instance.FsCMNUI);
        }
    }
    public void Exit()
    {
        if (selected)
            return;
        _selectionImage.enabled = false;
        _hoverImage.enabled = false;
        DespawnPlayerIndicator();
    }
    public void SpawnPlayerIndicator()
    {
        playerIndicator = Instantiate(_owner.Player.UiIndicatorPrefab, transform);
    }
    public void DespawnPlayerIndicator()
    {
        Destroy(playerIndicator);
    }
}
