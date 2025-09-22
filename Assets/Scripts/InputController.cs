using System.Buffers;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputController : MonoBehaviour
{
    private Player owner;
    [SerializeField] private PlayerInput _playerInput;


    private InputAction back;
    private InputAction block;
    private InputAction reshuffle;
    private InputAction reset;
    private InputAction quit;

    public Player Owner { get => owner; set => owner = value; }

    private void Start()
    {
        _playerInput.currentActionMap.Enable();
        back = _playerInput.currentActionMap.FindAction("Back");
        block = _playerInput.currentActionMap.FindAction("Block");
        reshuffle = _playerInput.currentActionMap.FindAction("Reshuffle");
        reset = _playerInput.currentActionMap.FindAction("Reset");
        quit = _playerInput.currentActionMap.FindAction("Quit");

        back.performed += Back_performed;
        block.started += Block_started;
        block.canceled += Block_canceled;
        reshuffle.performed += Reshuffle_performed;
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
    private void Block_started(InputAction.CallbackContext obj)
    {
        owner.CombatManager.StartBlocking();
    }
    private void Block_canceled(InputAction.CallbackContext obj)
    {
        owner.CombatManager.StopBlocking();
    }
    private void Reshuffle_performed(InputAction.CallbackContext obj)
    {
        owner.Game.ReshuffleBoard();
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
