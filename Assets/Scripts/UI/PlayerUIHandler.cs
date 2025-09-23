using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIHandler : MonoBehaviour
{
    private Player owner;
    [SerializeField] private TMP_Text _fighterNameText;

    [Header("HP Bar")]
    [SerializeField] private Image _HPBarFillImage;
    [SerializeField] private Gradient _HPBarGradient;
    [SerializeField] private float _HPBarFillSpeed = 3;
    private float activeHPFillSpeed;

    [Header("Super Bar")]
    [SerializeField] private Image _superBarFillImage;
    [SerializeField] private Gradient _superBarGradient;
    [SerializeField] private float _superBarFillSpeed = 3;
    private float activeSuperFillSpeed;

    private void Start()
    {
        owner = GetComponent<Player>();

        LinkFighterNameText();
    }
    private void FixedUpdate()
    {
        LinkHPToHPBar();
        LinkSuperToBar();
    }
    private void LinkFighterNameText()
    {
        _fighterNameText.text = owner.Fighter.Name;
    }
    public void LinkHPToHPBar()
    {
        float HPPercented = owner.CurrentHP / owner.Fighter.HP;
        activeHPFillSpeed = _HPBarFillSpeed * Time.deltaTime;
        _HPBarFillImage.fillAmount = Mathf.Lerp(_HPBarFillImage.fillAmount, HPPercented, activeHPFillSpeed);
        _HPBarFillImage.color = _HPBarGradient.Evaluate(HPPercented);
    }
    private void LinkSuperToBar()
    {
        float superPercented = owner.CurrentSuper / owner.Fighter.SuperCapacity;
        activeSuperFillSpeed = _superBarFillSpeed * Time.deltaTime;
        _superBarFillImage.fillAmount = Mathf.Lerp(_superBarFillImage.fillAmount, superPercented, activeSuperFillSpeed);
        _superBarFillImage.color = _superBarGradient.Evaluate(superPercented);
    }
}
