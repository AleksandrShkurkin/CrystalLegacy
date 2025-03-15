using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArmorType { Helmet, Chestplate, Pants, Boots }

public abstract class Armor : Item
{
    public ArmorType armorType;
    public float armorDefense;

    public abstract void SpecialEffect();
    public abstract void SpecialEffectRemove();
}
