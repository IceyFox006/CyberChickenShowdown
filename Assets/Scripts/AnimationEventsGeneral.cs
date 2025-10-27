using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationEventsGeneral : MonoBehaviour
{
    private Animator animator;

    private string sceneChange = "NextRound";

    public string SceneChange { get => sceneChange; set => sceneChange = value; }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerAnimation()
    {
        animator.SetTrigger("triggerAnimation");
    }
    public void ResetTrigger()
    {
        animator.ResetTrigger("triggerAnimation");
    }
    public void DestroyObject()
    {
        Destroy(gameObject);
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void LoadSceneChange()
    {
        Time.timeScale = 1;
        switch (sceneChange)
        {
            case "NextRound":
                GameManager.Instance.NextRound(); break;
            default:
                SceneManager.LoadScene(sceneChange); break;
        }
    }
}
