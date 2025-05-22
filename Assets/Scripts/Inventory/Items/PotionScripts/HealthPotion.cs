using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Health Potion Lvl", menuName = "Items/Potions/Health" )]
public class HealthPotion : Potion
{
    public override void UsePotion(Player.Player player)
    {
        int maxHealth = 100 + (int)(100 * (player.Level / 10.0f)) +
        (player.ActiveEffectType == EffectType.Health ? (int)player.activeEffectValue : 0);

        if (amountStacked > 0 && player.Health < maxHealth)
        {
            player.Health += potionEffectValue;
            if (player.Health > maxHealth)
            {
                player.Health = maxHealth;
            }
            amountStacked--;
        }
    }
}
