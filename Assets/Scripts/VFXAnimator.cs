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
    public void ResetElement()
    {
        _owner.CombatManager.AttackElementID = 0;
    }
}
