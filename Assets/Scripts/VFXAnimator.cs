using UnityEngine;

public class VFXAnimator : MonoBehaviour
{
    [SerializeField] private Player _owner;
    [SerializeField] private Animator _VFXAnimator;
    private void FixedUpdate()
    {
        UpdateVFXAnimation();
    }
    private void UpdateVFXAnimation()
    {
        _VFXAnimator.SetBool("isSTAB", _owner.CombatManager.IsSTAB);
        _VFXAnimator.SetBool("isSuper", _owner.CombatManager.IsSuper);
        _VFXAnimator.SetInteger("elementID", _owner.CombatManager.AttackElementID);
    }
    public void TriggerOpponentAnimation()
    {
        GameManager.Instance.GetOpponent(_owner).GameObjectController.FighterAnimator.TriggerAnimation();
    }
    public void EnactSuper()
    {
        _owner.CombatManager.UseSuper(GameManager.Instance.GetOpponent(_owner));
    }
    public void ResetTrigger()
    {
        _VFXAnimator.ResetTrigger("triggerAnimation");
    }
    public void ResetElement()
    {
        ResetTrigger();
        _owner.CombatManager.AttackElementID = 0;
    }
}
