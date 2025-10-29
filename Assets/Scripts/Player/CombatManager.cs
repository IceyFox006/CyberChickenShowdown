using System;
using System.Collections;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    private Player owner;
    private float legUp;

    private bool isBlocking = false;
    private bool isAttacking = false;
    private bool isSTAB = false;
    private bool isSuper = false;
    private bool isHurt = false;
    private bool isDead = false;

    private int attackElementID = 0;

    private bool superIsActive = false;

    //Tutorial
    public static Action SuperFillTaskComplete;
    private bool isSuperFillTaskComplete;

    public static Action UseSuperTaskComplete;
    private bool isUseSuperTaskComplete;

    public bool IsBlocking { get => isBlocking; set => isBlocking = value; }
    public bool IsAttacking { get => isAttacking; set => isAttacking = value; }
    public bool IsSTAB { get => isSTAB; set => isSTAB = value; }
    public bool IsSuper { get => isSuper; set => isSuper = value; }
    public bool IsHurt { get => isHurt; set => isHurt = value; }
    public bool IsDead { get => isDead; set => isDead = value; }
    public int AttackElementID { get => attackElementID; set => attackElementID = value; }

    private void Start()
    {
        owner = GetComponent<Player>();
    }
    public void AttackOpponent(Player target, Match match)
    {
        bool hasPlayedMatchSFX = false;

        float damage = owner.Data.Fighter.Attack;
        isAttacking = true;
        UpdateMatchElement(match);

        //STAB (if the owner's element matches the match element)
        if (match.Element == owner.Data.Fighter.Element)
        {
            damage *= GameManager.Instance.STABMultiplier;
            isSTAB = true;
            owner.AudioManager.PlaySound("STABMatch");
            hasPlayedMatchSFX = true;

            //FighterElementAttackBoost
            if (superIsActive && owner.Data.Fighter.SuperFunction == Enums.SuperFunction.FighterElementAttackBoost)
                damage *= 1 + (owner.Data.Fighter.SuperEffectiveness * 0.1f);
        }

        //Combo
        if (match.ConnectedPoints.Count > 3)
            damage *= (1 + ((match.ConnectedPoints.Count - 3) * GameManager.Instance.ComboAdditiveMultiplier));

        //Weakness
        for (int index = 0; index < target.Data.Fighter.Element.Weaknesses.Length; index++)
        {
            if (target.Data.Fighter.Element.Weaknesses[index] == match.Element)
                damage *= GameManager.Instance.WeaknessMultiplier;
        }

        //Resistance
        for (int index = 0; index < target.Data.Fighter.Element.Resistances.Length; index++)
        {
            if (target.Data.Fighter.Element.Resistances[index] == match.Element)
                damage *= GameManager.Instance.ResistanceMultiplier;
        }

        //OpponentBlocking
        if (target.CombatManager.IsBlocking)
            damage *= (1 - target.Data.Fighter.BlockEffectiveness);

        if (!hasPlayedMatchSFX)
            owner.AudioManager.PlaySound("Match");

        DealDamage(target, damage, true, match.Element);

        ChargeSuper(damage);
    }

    public void DealDamage(Player target, float damage, bool spawnFloatingText = false, ElementSO damageElement = null)
    {
        if (target.CombatManager.IsDead)
            return;

        target.CurrentHP -= damage;
        owner.UiHandler.DamageDealt += damage;
        CorrectHPAmount();


        if (spawnFloatingText)
        {
            if (damageElement == null)
                target.UiHandler.SpawnFloatingText(target.UiHandler.ReduceDamageFT, Mathf.RoundToInt(damage).ToString());
            else
                target.UiHandler.SpawnFloatingText(damageElement.DamageFT, Mathf.RoundToInt(damage).ToString(), damageElement == owner.Data.Fighter.Element);
        }

        if (target.CurrentHP <= 0)
        {
            target.CombatManager.IsDead = true;
            GameManager.Instance.CameraAnimator.Animator.SetBool("isDead", true);
            GameManager.Instance.CameraAnimator.Animator.SetInteger("PlayerID", target.Data.ID);
            GameManager.Instance.CameraAnimator.Animator.SetTrigger("triggerAnimation");
        }
        else
            target.CombatManager.IsHurt = true;

    }
    public void RegenHealth(Player target, float regenValue)
    {
        target.CurrentHP += regenValue;
        CorrectHPAmount();

        target.UiHandler.SpawnFloatingText(target.UiHandler.RegenHealthFT, Mathf.RoundToInt(regenValue).ToString());
    }
    public void UpdateMatchElement(Match match)
    {
        if (isSTAB || isSuper)
            return;
        attackElementID = (int)match.Element.Element;
    }
    public void CorrectHPAmount()
    {
        if (owner.CurrentHP > owner.Data.Fighter.HP)
            owner.CurrentHP = owner.Data.Fighter.HP;
    }

    private void ChargeSuper(float damage)
    {
        UpdateLegUp();

        owner.CurrentSuper += (damage * legUp * owner.Data.Fighter.SuperFillSpeed);
        CorrectSuperAmount();
    }
    public bool IsSuperFull()
    {
        if (owner.CurrentSuper >= owner.Data.Fighter.SuperCapacity)
        {
            if (PlayerMatch3.IsInTutorial() && !isSuperFillTaskComplete)
            {
                SuperFillTaskComplete?.Invoke();
                isSuperFillTaskComplete = true;
            }
            return true;
        }
        return false;
    }
    private void CorrectSuperAmount()
    {
        if (IsSuperFull())
        {
            owner.CurrentSuper = owner.Data.Fighter.SuperCapacity;
            return;
        }
        if (owner.CurrentSuper < 0)
            owner.CurrentSuper = 0;
    }
    private void UpdateLegUp()
    {
        if (owner.GetHPPercentage() > GameManager.Instance.GetOpponent(owner).GetHPPercentage())
        {
            legUp = 1f;
            owner.UiHandler.LegUpImage.enabled = false;
        }
        else
        {
            legUp = (1 + (owner.GetHPPercentage() / GameManager.Instance.GetOpponent(owner).GetHPPercentage()) * 0.5f) * GameManager.Instance.LegUpMultiplier;
            owner.UiHandler.LegUpImage.enabled = true;
        }
    }
    public void UseSuper(Player target)
    {
        if (!IsSuperFull())
            return;

        if (PlayerMatch3.IsInTutorial() && !isUseSuperTaskComplete)
        {
            UseSuperTaskComplete?.Invoke();
            isUseSuperTaskComplete = true;
        }
        float value = 0;
        target.UiHandler.SpawnFloatingText(target.UiHandler.SuperFT);
        owner.AudioManager.PlaySound("SuperActivate");
        switch (owner.Data.Fighter.SuperFunction)
        {
            case Enums.SuperFunction.ImmediateDamage:
                value = owner.Data.Fighter.Attack * owner.Data.Fighter.SuperEffectiveness;
                DealDamage(target, value);
                break;
            case Enums.SuperFunction.LeechOpponentSuperToHP:
                value = target.Data.Fighter.SuperCapacity * 0.3f;
                target.CurrentSuper -= value;
                target.CombatManager.CorrectSuperAmount();
                owner.CombatManager.RegenHealth(owner, value * (owner.Data.Fighter.SuperEffectiveness * 0.1f));
                break;
            case Enums.SuperFunction.Turn30PercentOfThisBoardToFighterElement:
                owner.Game.ChangePercentOfPiecesToElement(owner.Data.Fighter.Element, owner.Data.Fighter.SuperEffectiveness / 100f);
                break;
            case Enums.SuperFunction.HackOpponentBoard:
                owner.UiHandler.ActivateSuperVisual();
                target.Game.ChangeNumberOfPiecesToPiece(GameManager.Instance.VirusPiece, (int)(owner.Data.Fighter.SuperEffectiveness * 0.1f));
                StartCoroutine(target.Game.HackOpponentBoardSuperDuration((int)owner.Data.Fighter.SuperEffectiveness * 0.1f));
                break;
            case Enums.SuperFunction.FighterElementAttackBoost:
                owner.UiHandler.ActivateSuperVisual();
                superIsActive = true;
                StartCoroutine(SuperCountDown((int)(owner.Data.Fighter.SuperEffectiveness * 0.5f)));
                break;
        }
        owner.CurrentSuper = 0;
    }
    private IEnumerator SuperCountDown(int duration)
    {
        yield return new WaitForSeconds(duration);
        superIsActive = false;
        owner.UiHandler.DeactivateSuperVisual();
    }
    public void StartBlocking()
    {
        float blockThreshold = GameManager.Instance.BlockThreshold * owner.Data.Fighter.SuperDrainRate;
        if (owner.CurrentSuper < blockThreshold)
            return;
        owner.CurrentSuper -= blockThreshold;
        owner.CombatManager.IsBlocking = true;
        owner.GameObjectController.BlockVisualGO.GetComponent<SpriteRenderer>().enabled = true;
        owner.AudioManager.PlaySound("BlockActivate");
        StartCoroutine(Blocking());
    }
    private IEnumerator Blocking()
    {
        while (isBlocking)
        {
            owner.CurrentSuper -= owner.Data.Fighter.SuperDrainRate;
            if (owner.CurrentSuper <= 0)
                StopBlocking();
            yield return new WaitForSeconds(GameManager.Instance.Tick / GameManager.Instance.BlockDrainSpeed);
        }
    }
    public void StopBlocking()
    {
        owner.CombatManager.IsBlocking = false;
        owner.GameObjectController.BlockVisualGO.GetComponent<SpriteRenderer>().enabled = false;
    }
}
