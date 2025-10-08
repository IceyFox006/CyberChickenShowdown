using UnityEngine;

public class UISFX : MonoBehaviour
{
    private AudioManager playerAudio;
    public void PlaySFX(string name)
    {
        AudioManager.Instance.PlaySoundAt(transform.position, name);
    }
}
