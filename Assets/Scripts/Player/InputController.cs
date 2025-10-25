using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private Player owner;
    [SerializeField] private PlayerInput _playerInput;

    private InputAction back;
    private InputAction block;
    private InputAction reshuffle;
    private InputAction super;

    public Player Owner { get => owner; set => owner = value; }

    private void Start()
    {
        _playerInput.currentActionMap.Enable();
        back = _playerInput.currentActionMap.FindAction("Back");
        block = _playerInput.currentActionMap.FindAction("Block");
        reshuffle = _playerInput.currentActionMap.FindAction("Reshuffle");
        super = _playerInput.currentActionMap.FindAction("Super");


        back.performed += Back_performed;
        block.started += Block_started;
        block.canceled += Block_canceled;
        reshuffle.performed += Reshuffle_performed;
        super.performed += Super_performed;

    }
    private void OnDestroy()
    {
        back.performed -= Back_performed;
        block.started -= Block_started;
        block.canceled -= Block_canceled;
        reshuffle.performed -= Reshuffle_performed;
        super.performed -= Super_performed;
    }
    public void EnableInput()
    {
        _playerInput.currentActionMap.Enable();
    }
    public void DisableInput()
    {
        _playerInput.currentActionMap.Disable();
    }
    private void Back_performed(InputAction.CallbackContext obj)
    {
        if (owner.Game.IsSelecting)
            owner.Game.DeselectAllPieces();
        owner.Game.PieceMover.DropPiece();
    }
    private void Block_started(InputAction.CallbackContext obj)
    {
        //owner.CombatManager.StartBlocking();
    }
    private void Block_canceled(InputAction.CallbackContext obj)
    {
        //owner.CombatManager.StopBlocking();
    }
    private void Reshuffle_performed(InputAction.CallbackContext obj)
    {
        owner.UiHandler.ShowControlGuide();
        //owner.Game.ReshuffleBoard();
    }
    private void Super_performed(InputAction.CallbackContext obj)
    {
        if (!owner.CombatManager.IsSuperFull())
            return;
        GameManager.Instance.CameraAnimator.Animator.SetTrigger("triggerAnimation");
        GameManager.Instance.CameraAnimator.Animator.SetInteger("PlayerID", owner.Data.ID);
    }
}
