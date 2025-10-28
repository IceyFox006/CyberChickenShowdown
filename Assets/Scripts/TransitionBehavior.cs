using System.Collections;
using UnityEngine;

public class TransitionBehavior : MonoBehaviour
{
    private static TransitionBehavior instance;

    [SerializeField] private Animator _animator;
    [SerializeField] private AnimationEventsGeneral _events;
    [SerializeField] private float _loadWaitTime = 0.5f;

    public static TransitionBehavior Instance { get => instance; set => instance = value; }
    public AnimationEventsGeneral Events { get => _events; set => _events = value; }

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        StartCoroutine(LoadOpen());
    }
    private IEnumerator LoadOpen()
    {
        yield return new WaitForSeconds(1);
        _animator.Play("OpenAnimation");
    }
    public void PlayClose()
    {
        _animator.Play("CloseAnimation");
    }
    public void PlayClose(string scene)
    {
        Events.SceneChange = scene;
        PlayClose();
    }
}
