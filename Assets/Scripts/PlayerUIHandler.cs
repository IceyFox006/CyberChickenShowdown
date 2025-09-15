using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIHandler : MonoBehaviour
{
    private Player owner;
    [SerializeField] private TMP_Text _fighterNameText;
    [SerializeField] private Image _HPBarFillImage;
    [SerializeField] private Gradient _HPBarGradient;
    [SerializeField] private Image _superBarFillImage;
    [SerializeField] private Gradient _superBarGradient;

    private void Start()
    {
        owner = GetComponent<Player>();

        LinkFighterNameText();
        LinkHPToHPBar();
        LinkSuperToBar();
    }
    private void LinkFighterNameText()
    {
        _fighterNameText.text = owner.Fighter.Name;
    }
    private void LinkHPToHPBar()
    {
        float HPPercented = owner.CurrentHP / owner.Fighter.HP;
        _HPBarFillImage.fillAmount = HPPercented;
        _HPBarFillImage.color = _HPBarGradient.Evaluate(HPPercented);
    }
    private void LinkSuperToBar()
    {
        float SuperPercented = owner.CurrentSuper / owner.Fighter.SuperCapacity;
        _superBarFillImage.fillAmount = SuperPercented;
        _superBarFillImage.color = _superBarGradient.Evaluate(SuperPercented);
    }
}
