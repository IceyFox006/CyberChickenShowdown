using UnityEngine;

public class UISFX : MonoBehaviour
{
    private AudioManager playerAudio;
    public void PlaySFX(string name)
    {
        if (GetComponents<AudioManager>().Length > 1)
        {
            if (GetComponent<ActivePieceController>() != null)
                GetComponent<ActivePieceController>().Owner.AudioManager.PlaySound(name);
        }
        AudioManager.Instance.PlaySound(name);
        //AudioManager.Instance.PlaySoundAt(transform.position, name);
    }
}
