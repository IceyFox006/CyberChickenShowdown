using UnityEngine;

public class GameScreenAnimationEvents : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public void SetTrigger()
    {
        _animator.SetTrigger("triggerAnimation");
    }
    public void ResetTrigger()
    {
        _animator.ResetTrigger("triggerAnimation");
    }
    public void ShowUI()
    {
        GameManager.Instance.ShowUI();
    }
    public void HideUI()
    {
        GameManager.Instance.HideUI();
    }
    public void EnableAllInput()
    {
        GameManager.Instance.EnableAllInput();
    }
    public void DisableAllInput()
    {
        GameManager.Instance.DisableAllInput();
    }
    public void StartAnimationSequence()
    {
        HideUI();
        DisableAllInput();
        //Pause fighter animation
        //Play super activate VFX

        ResetTrigger();
    }
    public void StartFighterSuperAnimation()
    {
        ShowUI();
        Player owner = GameManager.Instance.GetPlayerFromID(_animator.GetInteger("PlayerID"));
        owner.CombatManager.AttackElementID = (int)owner.Data.Fighter.Element.Element;
        owner.CombatManager.IsSuper = true;
    }
    public void StartFighterDeathAnimation()
    {
        Player owner = GameManager.Instance.GetPlayerFromID(_animator.GetInteger("PlayerID"));
        owner.CombatManager.IsDead = true;
    }
}
