using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mana Potion Lvl", menuName = "Items/Potions/Mana" )]
public class ManaPotion : Potion
{
    public override void UsePotion(Player.Player player)
    {
        int maxMana = 100 + (player.ActiveEffectType == EffectType.Mana ? (int)player.activeEffectValue : 0);
        if (amountStacked > 0 && player.Mana < maxMana)
        {
            player.Mana += potionEffectValue;
            if (player.Mana > maxMana)
            {
                player.Mana = maxMana;
            }
            amountStacked--;
        }
    }
}
