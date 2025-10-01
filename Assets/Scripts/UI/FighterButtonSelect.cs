using UnityEngine;
using UnityEngine.UI;

public class FighterButtonSelect : MonoBehaviour
{
    [SerializeField] private SelectScreenBehaviors _ssb;
    [SerializeField] private Button _button;
    [SerializeField] private Image _selectionImage;
    [SerializeField] private Image _hoverImage;

    private bool selected = false;

    public void Hover()
    {
        if (selected)
            return;
        Exit();
        _hoverImage.enabled = true;
    }
    public void Select()
    {
        if (selected)
            return;
        Exit();
        _selectionImage.enabled = true;
        selected = true;
    }
    public void Exit()
    {
        if (selected)
            return;
        _selectionImage.enabled = false;
        _hoverImage.enabled = false;
    }
}
