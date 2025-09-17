using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputController : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    private Player owner;

    private InputAction back;
    private InputAction reset;
    private InputAction quit;

    public Player Owner { get => owner; set => owner = value; }

    private void Start()
    {
        _playerInput.currentActionMap.Enable();
        back = _playerInput.currentActionMap.FindAction("Back");
        reset = _playerInput.currentActionMap.FindAction("Reset");
        quit = _playerInput.currentActionMap.FindAction("Quit");

        back.performed += Back_performed;
        reset.performed += Reset_performed;
        quit.performed += Quit_performed;
    }
    private void OnDestroy()
    {
        back.performed -= Back_performed;
        reset.performed -= Reset_performed;
        quit.performed -= Quit_performed;
    }
    private void Back_performed(InputAction.CallbackContext obj)
    {
        owner.Game.PieceMover.DropPiece();
        owner.Game.DeselectAllPieces();
    }



    private void Reset_performed(InputAction.CallbackContext obj)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void Quit_performed(InputAction.CallbackContext obj)
    {
        Application.Quit();
    }
}
