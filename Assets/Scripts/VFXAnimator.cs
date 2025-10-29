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
        Player opponent = GameManager.Instance.GetOpponent(_owner);
        opponent.GameObjectController.FighterAnimator.TriggerAnimation();
        if (opponent.CombatManager.IsDead)
            ShakeCamera();
        TriggerElementSFX(_VFXAnimator.GetInteger("elementID"));
    }
    public void TriggerElementSFX(int elementID)
    {
        switch (elementID)
        {
            case 1: _owner.AudioManager.PlaySound("PlasmaMatch"); break;
            case 2: _owner.AudioManager.PlaySound("GravityMatch"); break;
            case 3: _owner.AudioManager.PlaySound("FireMatch"); break;
            case 4: _owner.AudioManager.PlaySound("HackMatch"); break;
            case 5: _owner.AudioManager.PlaySound("DirectMatch"); break;
        }
    }
    public void EnactSuper()
    {
        _owner.CombatManager.UseSuper(GameManager.Instance.GetOpponent(_owner));
    }
    public void ShakeCamera()
    {
        GameManager.Instance.CameraAnimator.ImpulseSource.GenerateImpulse();
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
