using UnityEngine;

public class PlayerGOController : MonoBehaviour
{
    [SerializeField] private GameObject _blockVisualGO;

    public GameObject BlockVisualGO { get => _blockVisualGO; set => _blockVisualGO = value; }
}
