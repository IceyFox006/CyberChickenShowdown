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


        target.CurrentHP -= damage;
        target.UiHandler.LinkHPToHPBar();
        Debug.Log(owner.Name + " dealt " + match.Element.Name + " " + damage + " to " + target.Name + "!");
    }
}
