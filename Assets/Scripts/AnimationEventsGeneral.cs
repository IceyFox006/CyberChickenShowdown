using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationEventsGeneral : MonoBehaviour
{
    public void DestroyObject()
    {
        Destroy(gameObject);
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
