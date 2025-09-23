using System.Collections;
using UnityEngine;
using static Enums;

public class CombatManager : MonoBehaviour
{
    private Player owner;

    private bool isBlocking = false;

    public bool IsBlocking { get => isBlocking; set => isBlocking = value; }

    private void Start()
    {
        owner = GetComponent<Player>();
    }
    public void AttackOpponent(Player target, Match match)
    {
        float damage = owner.Fighter.Attack;

        //STAB (if the owner's element matches the match element)
        if (match.Element == owner.Fighter.Element)
            damage *= GameManager.Instance.STABMultiplier;

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


        DealDamage(target, damage);
        //Debug.Log(owner.Name + " dealt " + match.Element.Name + " " + damage + " to " + target.Name + ".");

        ChargeSuper(damage);
    }

    private void DealDamage(Player target, float damage)
    {
        target.CurrentHP -= damage;
        Debug.Log(owner.Name + " dealt " + damage + " to " + target.Name);
        if (target.CurrentHP <= 0)
            GameManager.Instance.EndGame();
    }

    private void ChargeSuper(float damage)
    {
        owner.CurrentSuper += (damage * owner.Fighter.SuperFillSpeed);
        CorrectSuperAmount();
    }
    private bool IsSuperFull()
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
            case Enums.SuperFunction.ImmediateSuperDrain:
                value = owner.Fighter.SuperEffectiveness;
                target.CurrentSuper -= value;
                target.CombatManager.CorrectSuperAmount();
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
