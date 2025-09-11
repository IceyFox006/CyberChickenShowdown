using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private InputController _inputController;
    [SerializeField] private PlayerMatch3 _game;

    public PlayerMatch3 Game { get => _game; set => _game = value; }
    public string Name { get => _name; set => _name = value; }

    private void Awake()
    {
        _game.Owner = this;
        _inputController.Owner = this;
    }
}
