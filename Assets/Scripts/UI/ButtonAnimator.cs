using UnityEngine;

public class ButtonAnimator : MonoBehaviour
{
    private bool hasSelected = false;
    public enum Function
    {
        None,
        Tutorial,
        Credits,
        Exit,
        Rounds1,
        Rounds3,
        Rounds5,
        GameScreen,
        CharacterSelect,
        TitleScreen,
        Resume,
        GameControls,
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
        if (hasSelected)
            return;
        if (!facingRight)
            _animator.Play("HOVER_EXIT");
        else
            _animator.Play("HOVER_EXIT(R)");
    }
    public void PlaySelectAnimation()
    {
        hasSelected = true;
        if (!facingRight)
            _animator.Play("SELECT");
        else
            _animator.Play("SELECT(R)");
    }
    public void ActivateFunction()
    {
        hasSelected = false;
        switch (function)
        {
            case Function.Tutorial:
                TitleScreenBehavior tsb = FindFirstObjectByType<TitleScreenBehavior>();
                tsb.Player1.Fighter = tsb.TestDummy;
                tsb.Player2.Fighter = tsb.TestDummy;
                TransitionBehavior.Instance.PlayClose("TutorialScene");
            //tsb.OpenTutorial();
                break;
            case Function.Exit:
                Application.Quit();
                #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                #endif
                break;
            case Function.Rounds1: 
                FindFirstObjectByType<SelectScreenBehavior>().SetRoundCount(1); break;
            case Function.Rounds3: 
                FindFirstObjectByType<SelectScreenBehavior>().SetRoundCount(3); break;
            case Function.Rounds5: 
                FindFirstObjectByType<SelectScreenBehavior>().SetRoundCount(5); break;
            case Function.CharacterSelect:
                TransitionBehavior.Instance.PlayClose("CharacterSelectScreen"); break;
            case Function.GameScreen:
                TransitionBehavior.Instance.PlayClose("GameScreen"); 
                break;
            case Function.TitleScreen:
                TransitionBehavior.Instance.PlayClose("TitleScreen"); break;
            case Function.Resume:
                GameManager.Instance.ResumeGame(); break;
            case Function.GameControls:
                PauseScreenBehavior.Instance.OpenUniversalControls(); break;

            case Function.None:
                Debug.LogError("No function assigned to " + gameObject.name + "'s button animator."); break;
            default:
                Debug.LogError(gameObject.name + "'s function has not been implemented yet."); break;
        }
    }

}
