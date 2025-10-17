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
        Rounds1,
        Rounds3,
        Rounds5,
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
            case Function.Tutorial: FindFirstObjectByType<TitleScreenBehavior>().OpenTutorial(); break;
            case Function.Exit:
                Application.Quit();
                #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                #endif
                break;
            case Function.Rounds1: FindFirstObjectByType<SelectScreenBehavior>().SetRoundCount(1); break;
            case Function.Rounds3: FindFirstObjectByType<SelectScreenBehavior>().SetRoundCount(3); break;
            case Function.Rounds5: FindFirstObjectByType<SelectScreenBehavior>().SetRoundCount(5); break;
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
