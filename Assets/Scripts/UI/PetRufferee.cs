using System.Collections;
using UnityEngine;

public class PetRufferee : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private Vector2 previousMousePosition = Vector2.zero;
    private Vector2 currentMousePosition = Vector2.zero;
    private void Start()
    {
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
            yield return new WaitForSeconds(0.5f);
            RaycastHit2D hit = RaycastOn2DMousePosition();
            previousMousePosition = currentMousePosition;
            currentMousePosition = Input.mousePosition;
            if (hit && MouseIsMoving())
            {
                _animator.Play("MOVING_HOVER");
                Debug.Log("Hit " + hit.collider.gameObject.name);
            }
            else if (hit && !MouseIsMoving())
            {
                _animator.Play("RESTING_HOVER");
            }
            else
            {
                _animator.Play("IDLE");
            }
        }
    }
}
