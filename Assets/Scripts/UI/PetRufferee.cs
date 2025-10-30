using System.Collections;
using UnityEngine;

public class PetRufferee : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private Animator dialogueAnimator;

    private Vector2 previousMousePosition = Vector2.zero;
    private Vector2 currentMousePosition = Vector2.zero;
    private void Start()
    {
        dialogueAnimator = FindFirstObjectByType<DialogueHandler>().Animator;
        StartCoroutine(CheckMovingDelay()); 
    }
    private RaycastHit2D RaycastOn2DMousePosition()
    {
        return Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
    }
    private bool MouseIsMoving()
    {
        return Mathf.Abs(previousMousePosition.x - currentMousePosition.x) > 1 || Mathf.Abs(previousMousePosition.y - currentMousePosition.y) > 1; 
    }
    private IEnumerator CheckMovingDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);
            if (dialogueAnimator.GetBool("IsOpen"))
                _animator.SetBool("isTalking", true);
            else
                _animator.SetBool("isTalking", false);

            RaycastHit2D hit = RaycastOn2DMousePosition();
            previousMousePosition = currentMousePosition;
            currentMousePosition = Input.mousePosition;
            if (hit && MouseIsMoving())
                _animator.SetBool("isPetting", true);
            else
                _animator.SetBool("isPetting", false);
                //_animator.Play("PET");
                //else if (hit && !MouseIsMoving())
                //{
                //    _animator.Play("RESTING_HOVER");
                //}
        }
    }
}
