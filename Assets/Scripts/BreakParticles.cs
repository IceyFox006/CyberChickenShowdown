using UnityEngine;

public class BreakParticles : MonoBehaviour
{
    private void FixedUpdate()
    {
        if (GetComponent<ParticleSystem>().isStopped)
            Destroy(gameObject);
    }
}
