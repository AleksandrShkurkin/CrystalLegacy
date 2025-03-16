using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Strength Potion Lvl", menuName = "Items/Potions/Strength" )]
public class StrengthPotion : Potion
{
    private CooldownManager strengthCooldown = new CooldownManager(5.1f);
    public override void UsePotion(Player player)
    {
        if (amountStacked > 0 && strengthCooldown.IsCooldownFinished(Time.time))
        {
            player.AttackDamage += potionEffectValue;
            strengthCooldown.InitiateCooldown(Time.time);
            player.StartCoroutine(ResetStrength(player));
            amountStacked--;
        }
    }

    private IEnumerator ResetStrength(Player player)
    {
        yield return new WaitForSeconds(5f);
        player.AttackDamage -= potionEffectValue;
    }
}
