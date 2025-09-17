using UnityEngine;

public class CombatManager : MonoBehaviour
{
    private Player owner;

    private void Start()
    {
        owner = GetComponent<Player>();
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

        target.CurrentHP -= damage;
        //Debug.Log(owner.Name + " dealt " + match.Element.Name + " " + damage + " to " + target.Name + ".");

        ChargeSuper(damage);
    }
    private void ChargeSuper(float damage)
    {
        owner.CurrentSuper += (damage / 10);
    }
}
