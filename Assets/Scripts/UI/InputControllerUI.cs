using UnityEngine;
using UnityEngine.InputSystem;

public class InputControllerUI : MonoBehaviour
{
    [SerializeField] private PlayerSelectScreen _owner;
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
        //Has not selected fighter. Return to title.
        if (!_owner.HasSelected)
        {
            TransitionBehavior.Instance.PlayClose("TitleScreen");
            return;
        }

        //Has selected fighter. Unselect fighter.
        if (_owner.HasSelected && !SelectScreenBehavior.Instance.HaveBothPlayersSelected())
        {
            _owner.UnselectFighter();
            return;
        }

        //Is selecting lives. Unselect both fighters.
        if (SelectScreenBehavior.Instance.HaveBothPlayersSelected())
        {
            SelectScreenBehavior.Instance.Player1.UnselectFighter();
            SelectScreenBehavior.Instance.Player2.UnselectFighter();
            SelectScreenBehavior.Instance.ChooseMatchNumberUIGO.SetActive(false);
            return;
        }
    }
}
