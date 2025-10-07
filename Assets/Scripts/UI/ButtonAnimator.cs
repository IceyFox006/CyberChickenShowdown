using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonAnimator : MonoBehaviour
{
    public enum Function
    {
        None,
        Play,
        Tutorial,
        Credits,
        Exit,
    }
    [SerializeField] private Function function;

    [SerializeField] private Animator _animator;

    public void PlayDormantAnimation()
    {
        _animator.Play("DORMANT");
    }
    public void PlayHoverEnterAnimation()
    {
        _animator.Play("HOVER_ENTER");
    }
    public void PlayHoverExitAnimation()
    {
        _animator.Play("HOVER_EXIT");
    }
    public void PlaySelectAnimation()
    {
        _animator.Play("SELECT");
    }
    public void ActivateFunction()
    {
        switch (function)
        {
            case Function.Play: SceneManager.LoadScene("CharacterSelectScreen"); break;

            case Function.None:
                Debug.LogError("No function assigned to " + gameObject.name + "'s button animator."); break;
            default:
                Debug.LogError(gameObject.name + "'s function has not been implemented yet."); break;
        }
    }
}
