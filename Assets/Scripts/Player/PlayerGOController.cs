using UnityEngine;

public class PlayerGOController : MonoBehaviour
{
    [SerializeField] private Player _owner;
    [SerializeField] private FighterAnimator _fighterAnimator;
    [SerializeField] private GameObject _blockVisualGO;

    public GameObject BlockVisualGO { get => _blockVisualGO; set => _blockVisualGO = value; }
    public FighterAnimator FighterAnimator { get => _fighterAnimator; set => _fighterAnimator = value; }
}
