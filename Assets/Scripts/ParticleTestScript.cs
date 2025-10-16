using UnityEngine;

public class ParticleTestScript : MonoBehaviour
{
    [SerializeField] private ParticleSystem m_ParticleSystem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_ParticleSystem.Play();
        }
    }
}
