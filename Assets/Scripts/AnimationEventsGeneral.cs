using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationEventsGeneral : MonoBehaviour
{
    private Animator animator;

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
    public void NextRound()
    {
        GameManager.Instance.NextRound();
    }
}
