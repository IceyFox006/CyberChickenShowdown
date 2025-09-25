using UnityEngine;

public class FighterAnimator : MonoBehaviour
{
    [SerializeField] private Player _owner;
    [SerializeField] private Animator _fighterAnimator;
    [SerializeField] private Animator _VFXAnimator;
    private void Start()
    {
        //_fighterAnimator = GetComponent<Animator>();

        _fighterAnimator.runtimeAnimatorController = _owner.Fighter.AnimationController;
    }
    private void FixedUpdate()
    {
        UpdateAnimation();
    }
    private void UpdateAnimation()
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
