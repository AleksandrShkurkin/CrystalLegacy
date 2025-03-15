using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mana Potion Lvl", menuName = "Items/Potions/Mana" )]
public class ManaPotion : Potion
{
    public override void UsePotion(Player player)
    {
        if (amountStacked > 0 && player.Mana < 100)
        {
            player.Mana += potionEffectValue;
            if (player.Mana > 100)
            {
                player.Mana = 100;
            }
            amountStacked--;
        }
    }
}
