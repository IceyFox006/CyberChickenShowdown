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
        Rematch,
        CharacterSelect,
        TitleScreen,
    }
    [SerializeField] private Function function;

    [SerializeField] private Animator _animator;
    [SerializeField] private bool facingRight;

    public void PlayDormantAnimation()
    {
        _animator.Play("DORMANT");
    }
    public void PlayHoverEnterAnimation()
    {
        if (!facingRight)
            _animator.Play("HOVER_ENTER");
        else
            _animator.Play("HOVER_ENTER(R)");
    }
    public void PlayHoverExitAnimation()
    {
        if (!facingRight)
            _animator.Play("HOVER_EXIT");
        else
            _animator.Play("HOVER_EXIT(R)");
    }
    public void PlaySelectAnimation()
    {
        if (!facingRight)
            _animator.Play("SELECT");
        else
            _animator.Play("SELECT(R)");
    }
    public void ActivateFunction()
    {
        switch (function)
        {
            case Function.Play: SceneManager.LoadScene("CharacterSelectScreen"); break;
            case Function.Tutorial: FindObjectOfType<TitleScreenBehavior>().OpenTutorial(); break;
            case Function.CharacterSelect: SceneManager.LoadScene("CharacterSelectScreen"); break;
            case Function.Rematch: SceneManager.LoadScene("GameScreen"); break;
            case Function.TitleScreen: SceneManager.LoadScene("TitleScreen"); break;

            case Function.None:
                Debug.LogError("No function assigned to " + gameObject.name + "'s button animator."); break;
            default:
                Debug.LogError(gameObject.name + "'s function has not been implemented yet."); break;
        }
    }

}
