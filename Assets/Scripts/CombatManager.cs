using System.Collections;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    private Player owner;

    private bool isBlocking = false;

    public bool IsBlocking { get => isBlocking; set => isBlocking = value; }

    private void Start()
    {
        owner = GetComponent<Player>();
    }
    private void Update()
    {
    }
    public void DealDamage(Player target, Match match)
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

        target.CurrentHP -= damage;
        Debug.Log(owner.Name + " dealt " + match.Element.Name + " " + damage + " to " + target.Name + ".");

        ChargeSuper(damage);
    }
    private void ChargeSuper(float damage)
    {
        owner.CurrentSuper += (damage * owner.Fighter.SuperFillSpeed);
        if (owner.CurrentSuper > owner.Fighter.SuperCapacity)
            owner.CurrentSuper = owner.Fighter.SuperCapacity;
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
