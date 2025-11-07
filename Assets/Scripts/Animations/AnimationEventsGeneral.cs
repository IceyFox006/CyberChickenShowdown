using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
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

    public void EnableAllInput()
    {
        EventSystem[] events = FindObjectsByType<EventSystem>(FindObjectsSortMode.None);
        MultiplayerEventSystem[] mEvents = FindObjectsByType<MultiplayerEventSystem>(FindObjectsSortMode.None);

        foreach (EventSystem e in events)
            e.sendNavigationEvents = true;
        foreach (MultiplayerEventSystem e in mEvents)
            e.sendNavigationEvents = true;
    }
    public void DisableAllInput()
    {
        EventSystem[] events = FindObjectsByType<EventSystem>(FindObjectsSortMode.None);
        MultiplayerEventSystem[] mEvents = FindObjectsByType<MultiplayerEventSystem>(FindObjectsSortMode.None);

        foreach (EventSystem e in events)
            e.sendNavigationEvents = false;
        foreach (MultiplayerEventSystem e in mEvents)
            e.sendNavigationEvents = false;
    }
    public void TriggerAnimation()
    {
        animator.SetTrigger("triggerAnimation");
    }
    public void ResetTrigger()
    {
        animator.ResetTrigger("triggerAnimation");
    }
    public void PlaySFX(string name)
    {
        AudioManager.Instance.PlaySound(name);
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
    public void PlayIntroSequence()
    {
        if (GameManager.Instance == null)
            return;
        GameManager.Instance.IntroSequenceAnimator.SetInteger("Round", StaticData.CurrentMatchCount);
        GameManager.Instance.IntroSequenceAnimator.SetTrigger("Go");
    }
}
