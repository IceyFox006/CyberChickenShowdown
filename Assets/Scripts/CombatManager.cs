using System.Collections;
using UnityEngine;
using static Enums;

public class CombatManager : MonoBehaviour
{
    private Player owner;

    private bool isBlocking = false;
    private bool isAttacking = false;
    private bool isSTAB = false;
    private bool isSuper = false;
    private bool isHurt = false;
    private bool isDead = false;

    private int attackElementID = 0;

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
        float damage = owner.Fighter.Attack;
        isAttacking = true;
        UpdateMatchElement(match);

        //STAB (if the owner's element matches the match element)
        if (match.Element == owner.Fighter.Element)
        {
            damage *= GameManager.Instance.STABMultiplier;
            isSTAB = true;
        }

        //Combo
        if (match.ConnectedPoints.Count > 3)
            damage *= (1 + ((match.ConnectedPoints.Count - 3) * GameManager.Instance.ComboAdditiveMultiplier));

        //Weakness
        for (int index = 0; index < target.Fighter.Element.Weaknesses.Length; index++)
        {
            if (target.Fighter.Element.Weaknesses[index] == match.Element)
                damage *= GameManager.Instance.WeaknessMultiplier;
        }

        //Resistance
        for (int index = 0; index < target.Fighter.Element.Resistances.Length; index++)
        {
            if (target.Fighter.Element.Resistances[index] == match.Element)
                damage *= GameManager.Instance.ResistanceMultiplier;
        }

        //OpponentBlocking
        if (target.CombatManager.IsBlocking)
            damage *= (1 - target.Fighter.BlockEffectiveness);


        DealDamage(target, damage, true);
        //Debug.Log(owner.Name + " dealt " + match.Element.Name + " " + damage + " to " + target.Name + ".");

        ChargeSuper(damage);
    }

    private void DealDamage(Player target, float damage, bool spawnFloatingText = false)
    {
        target.CurrentHP -= damage;
        target.CombatManager.IsHurt = true;
        //Debug.Log(owner.Name + " dealt " + damage + " to " + target.Name);
        if (spawnFloatingText)
            target.UiHandler.SpawnFloatingText(target.UiHandler.NeutralHitFT, damage.ToString());
        if (target.CurrentHP <= 0)
            target.CombatManager.IsDead = true;
    }
    public void UpdateMatchElement(Match match)
    {
        attackElementID = (int)match.Element.Element;
    }
    public void CorrectHPAmount()
    {
        if (owner.CurrentHP > owner.Fighter.HP)
            owner.CurrentHP = owner.Fighter.HP;
    }

    private void ChargeSuper(float damage)
    {
        owner.CurrentSuper += (damage * owner.Fighter.SuperFillSpeed);
        CorrectSuperAmount();
    }
    public bool IsSuperFull()
    {
        if (owner.CurrentSuper >= owner.Fighter.SuperCapacity)
            return true;
        return false;
    }
    private void CorrectSuperAmount()
    {
        if (IsSuperFull())
        {
            owner.CurrentSuper = owner.Fighter.SuperCapacity;
            return;
        }
        if (owner.CurrentSuper < 0)
            owner.CurrentSuper = 0;
    }
    public void UseSuper(Player target)
    {
        if (!IsSuperFull())
            return;

        float value = 0;
        switch (owner.Fighter.SuperFunction)
        {
            case Enums.SuperFunction.ImmediateDamage:
                value = owner.Fighter.Attack * owner.Fighter.SuperEffectiveness;
                DealDamage(target, value);
                break;
            case Enums.SuperFunction.LeechOpponentSuperToHP:
                value = target.Fighter.SuperCapacity * 0.3f;
                target.CurrentSuper -= value;
                target.CombatManager.CorrectSuperAmount();
                owner.CurrentHP += value * (owner.Fighter.SuperEffectiveness * 0.1f);
                CorrectHPAmount();
                break;
        }
        owner.CurrentSuper = 0;
    }
    public void StartBlocking()
    {
        float blockThreshold = GameManager.Instance.BlockThreshold * owner.Fighter.SuperDrainRate;
        if (owner.CurrentSuper < blockThreshold)
            return;
        owner.CurrentSuper -= blockThreshold;
        owner.CombatManager.IsBlocking = true;
        owner.GameObjectController.BlockVisualGO.GetComponent<SpriteRenderer>().enabled = true;
        StartCoroutine(Blocking());
    }
    private IEnumerator Blocking()
    {
        while (isBlocking)
        {
            owner.CurrentSuper -= owner.Fighter.SuperDrainRate;
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
