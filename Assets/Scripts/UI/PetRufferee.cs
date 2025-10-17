using UnityEngine;

public class PetRufferee : MonoBehaviour
{
    private void FixedUpdate()
    {
        RaycastHit2D hit = RaycastOn2DMousePosition();
        if (hit)
        {
            Debug.Log("Hit " + hit.collider.gameObject.name);
        }
    }
    private RaycastHit2D RaycastOn2DMousePosition()
    {
        return Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
    }
}
