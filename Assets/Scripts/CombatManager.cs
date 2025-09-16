using UnityEngine;

public class CombatManager : MonoBehaviour
{
    private Player owner;


    public void DealDamage(Player target, Match match)
    {
        float damage = owner.Fighter.Attack;

        //STAB (if the owner's element matches the match element)
        if (match.Element == owner.Fighter.Element)
            damage *= GameManager.Instance.STABMultiplier;

        target.CurrentHP -= damage;
        target.UiHandler.LinkHPToHPBar();
    }
}
