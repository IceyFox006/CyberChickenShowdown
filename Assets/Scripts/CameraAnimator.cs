using Unity.Cinemachine;
using UnityEngine;

public class CameraAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private CinemachineImpulseSource _impulseSource;

    public Animator Animator { get => _animator; set => _animator = value; }
    public CinemachineImpulseSource ImpulseSource { get => _impulseSource; set => _impulseSource = value; }

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
    public void FreezeTime()
    {
        Time.timeScale = 0f;
    }
    public void SlowTime()
    {
        Time.timeScale = 0.5f;
    }
    public void NormalTime()
    {
        Time.timeScale = 1;
    }
    public void EndZoomIn()
    {
        ResetTrigger();
        NormalTime();
    }
    public void StartAnimationSequence()
    {
        GameManager.Instance.IsTimerGoing = false;
        HideUI();
        DisableAllInput();
        FreezeTime();
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
