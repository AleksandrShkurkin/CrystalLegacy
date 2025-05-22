using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PotionType { Health, Mana, Speed, Strength }

public abstract class Potion : Item
{
    public PotionType potionType;
    public int potionEffectValue;
    public int amountStacked;

    public abstract void UsePotion(Player.Player player);
}
