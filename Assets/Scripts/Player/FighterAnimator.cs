using UnityEngine;

public class FighterAnimator : MonoBehaviour
{
    [SerializeField] private Player _owner;
    [SerializeField] private Animator _fighterAnimator;
    [SerializeField] private Animator _VFXAnimator;

    public Animator Animator { get => _fighterAnimator; set => _fighterAnimator = value; }

    private void Start()
    {
        _fighterAnimator.runtimeAnimatorController = _owner.Data.Fighter.AnimationController;
        _fighterAnimator.SetInteger("PlayerID", _owner.Data.ID);
    }
    private void FixedUpdate()
    {
        UpdateFighterAnimation();
    }
    private void UpdateFighterAnimation()
    {
        _fighterAnimator.SetBool("isAttacking", _owner.CombatManager.IsAttacking);
        _fighterAnimator.SetBool("isSTAB", _owner.CombatManager.IsSTAB);
        _fighterAnimator.SetBool("isSuper", _owner.CombatManager.IsSuper);
        _fighterAnimator.SetBool("isHurt", _owner.CombatManager.IsHurt);
        _fighterAnimator.SetBool("isDead", _owner.CombatManager.IsDead);
    }
    public void EnactSuper()
    {
        _owner.CombatManager.UseSuper(GameManager.Instance.GetOpponent(_owner));
    }
    public void EndSuper()
    {
        EndAttacking();
        GameManager.Instance.EnableAllInput();
        GameManager.Instance.IsTimerGoing = true;
    }
    public void EndAttacking()
    {
        _owner.CombatManager.IsAttacking = false;
        _owner.CombatManager.IsSTAB = false;
        _owner.CombatManager.IsSuper = false;
    }
    public void EndHurt()
    {
        _fighterAnimator.ResetTrigger("triggerAnimation");
        _owner.CombatManager.IsHurt = false;
    }
    public void TriggerAnimation()
    {
        _fighterAnimator.SetTrigger("triggerAnimation");
    }
    public void TriggerVFXAnimation()
    {
        _VFXAnimator.SetTrigger("triggerAnimation");
    }
    public void DisableAllInput()
    {
        GameManager.Instance.DisableAllInput();
    }
    public void PlayTransitionClose()
    {
        GameManager.Instance.PlayCloseTransition(GameManager.Instance.GetOpponent(_owner));
    }
}
