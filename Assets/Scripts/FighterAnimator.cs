using UnityEngine;

public class FighterAnimator : MonoBehaviour
{
    [SerializeField] private Player _owner;
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();

        animator.runtimeAnimatorController = _owner.Fighter.AnimationController;
    }
    private void FixedUpdate()
    {
        UpdateAnimation();
    }
    private void UpdateAnimation()
    {
        animator.SetBool("isAttacking", _owner.CombatManager.IsAttacking);
        animator.SetBool("isSTAB", _owner.CombatManager.IsSTAB);
        animator.SetBool("isSuper", _owner.CombatManager.IsSuper);
        animator.SetBool("isHurt", _owner.CombatManager.IsHurt);
        animator.SetBool("isDead", _owner.CombatManager.IsDead);
    }
    public void EnactSuper()
    {
        _owner.CombatManager.UseSuper(GameManager.Instance.GetOpponent(_owner));
    }
    public void EndAttacking()
    {
        _owner.CombatManager.IsAttacking = false;
        _owner.CombatManager.IsSTAB = false;
        _owner.CombatManager.IsSuper = false;
    }
    public void EndHurt()
    {
        _owner.CombatManager.IsHurt = false;
    }
    public void EnactDie()
    {
        //_owner.CombatManager.IsDead = false;
        GameManager.Instance.EndGame();
    }
}
