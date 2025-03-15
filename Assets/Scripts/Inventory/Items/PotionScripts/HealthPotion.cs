using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Health Potion Lvl", menuName = "Items/Potions/Health" )]
public class HealthPotion : Potion
{
    public override void UsePotion(Player player)
    {
        if (amountStacked > 0 && player.Health < 100 + (int)(100 * (player.Level / 10.0f)))
        {
            player.Health += potionEffectValue;
            if (player.Health > 100 + (int)(100 * (player.Level / 10.0f)))
            {
                player.Health = 100 + (int)(100 * (player.Level / 10.0f));
            }
            amountStacked--;
        }
    }
}
