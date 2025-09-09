using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputController : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;

    private InputAction reset;
    private InputAction quit;

    private void Start()
    {
        _playerInput.currentActionMap.Enable();
        reset = _playerInput.currentActionMap.FindAction("Reset");
        quit = _playerInput.currentActionMap.FindAction("Quit");

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
        Application.Quit();
    }
}
