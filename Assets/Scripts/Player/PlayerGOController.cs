using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerGOController : MonoBehaviour
{
    [SerializeField] private Player _owner;
    [SerializeField] private FighterAnimator _fighterAnimator;
    [SerializeField] private GameObject _blockVisualGO;
    [SerializeField] private Light2D _spotLight;

    public GameObject BlockVisualGO { get => _blockVisualGO; set => _blockVisualGO = value; }
    public FighterAnimator FighterAnimator { get => _fighterAnimator; set => _fighterAnimator = value; }

    private void Start()
    {
        _spotLight.color = _owner.Data.Fighter.GlowColor;
    }
}
