using UnityEngine;
using UnityEngine.InputSystem;

public class InputControllerUI : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;

    private InputAction back;

    private void Start()
    {
        back = _playerInput.currentActionMap.FindAction("Back");
        back.performed += Back_performed;
    }
    private void OnDestroy()
    {
        back.performed -= Back_performed;
    }
    private void Back_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("Back");
    }
}
