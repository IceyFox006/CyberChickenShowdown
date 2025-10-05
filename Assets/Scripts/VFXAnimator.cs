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
        _VFXAnimator.SetInteger("elementID", _owner.CombatManager.AttackElementID);
    }
    public void TriggerOpponentAnimation()
    {
        GameManager.Instance.GetOpponent(_owner).GameObjectController.FighterAnimator.TriggerAnimation();
    }
    public void ResetElement()
    {
        _VFXAnimator.ResetTrigger("triggerAnimation");
        _owner.CombatManager.AttackElementID = 0;
    }
}
