using UnityEngine;
using UnityEngine.Events;

public class RoundTransitionAnimator : MonoBehaviour
{
    private static RoundTransitionAnimator instance;
    [SerializeField] private Animator _animator;

    private UnityEvent transitionEvent;
    private UnityAction currentFunction;

    public static RoundTransitionAnimator Instance { get => instance; set => instance = value; }
    public UnityEvent TransitionEvent { get => transitionEvent; set => transitionEvent = value; }
    public UnityAction CurrentFunction { get => currentFunction; set => currentFunction = value; }

    private void Awake()
    {
        instance = this;
    }
    public void ActivateFunction()
    {
        currentFunction.Invoke();
    }
    public void OpenCharacterSelect()
    {
        SelectScreenBehavior.Instance.Player1.UnselectFighter();
        SelectScreenBehavior.Instance.Player2.UnselectFighter();
        SelectScreenBehavior.Instance.ChooseMatchNumberUIGO.SetActive(false);

        currentFunction -= OpenCharacterSelect;
    }
    public void OpenVariantSelect()
    {

    }
    public void OpenRounds()
    {
        SelectScreenBehavior.Instance.ChooseMatchNumberUIGO.SetActive(true);
        SelectScreenBehavior.Instance.UniversalEventSystem.SetSelectedGameObject(SelectScreenBehavior.Instance.FsCMNUI);

        currentFunction -= OpenRounds;
    }
    public void PlayTransition()
    {
        _animator.Play("TransitionAnimation");
    }
}
