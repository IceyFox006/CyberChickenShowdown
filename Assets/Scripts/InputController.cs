using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputController : MonoBehaviour
{
    [SerializeField] private PlayerInput _player1Input;
    [SerializeField] private PlayerInput _player2Input;

    private InputAction reset;
    private InputAction quit;

    private void Start()
    {
        _player1Input.currentActionMap.Enable();
        reset = _player1Input.currentActionMap.FindAction("Reset");
        quit = _player1Input.currentActionMap.FindAction("Quit");

        reset.performed += Reset_performed;
        quit.performed += Quit_performed;
    }
    private void OnDestroy()
    {
        reset.performed -= Reset_performed;
        quit.performed -= Quit_performed;
    }

    private void Reset_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("Reset");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void Quit_performed(InputAction.CallbackContext obj)
    {

    }
}
