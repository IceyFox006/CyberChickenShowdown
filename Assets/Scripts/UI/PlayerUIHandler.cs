using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIHandler : MonoBehaviour
{
    private Player owner;
    [SerializeField] private TMP_Text _fighterNameText;
    [SerializeField] private Image _superVisualImage;

    [Header("Floating Text")]
    [SerializeField] private Transform _overlayCanvas;
    [SerializeField] private GameObject _floatingTextPrefab;
    [SerializeField] private Transform _floatingTextSpawnLocation;
    [SerializeField] private FloatingText _reduceDamageFT;
    [SerializeField] private FloatingText _regenHealthFT;

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

    public FloatingText ReduceDamageFT { get => _reduceDamageFT; set => _reduceDamageFT = value; }
    public FloatingText RegenHealthFT { get => _regenHealthFT; set => _regenHealthFT = value; }

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
    public void SpawnFloatingText(FloatingText floatingText, string externalText = "")
    {
        GameObject floatingTextGO = Instantiate(_floatingTextPrefab, _floatingTextSpawnLocation.position, Quaternion.identity, _overlayCanvas);
        TMP_Text text = floatingTextGO.GetComponentInChildren<TMP_Text>();
        if (floatingText.IsInternal)
            text.text = floatingText.Text;
        else
            text.text = externalText;
        text.color = new Color(floatingText.Color.r, floatingText.Color.g, floatingText.Color.b);
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
    public void ActivateSuperVisual()
    {
        if (owner.Fighter.SuperVisual == null)
            return;
        _superVisualImage.enabled = true;
        _superVisualImage.sprite = owner.Fighter.SuperVisual;
    }
    public void DeactivateSuperVisual()
    {
        _superVisualImage.enabled = false;
    }
}
