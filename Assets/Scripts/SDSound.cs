using UnityEngine;

public class SDSound : MonoBehaviour
{
    private void FixedUpdate()
    {
        if (!GetComponent<AudioSource>().isPlaying)
            Destroy(gameObject);
    }
}
