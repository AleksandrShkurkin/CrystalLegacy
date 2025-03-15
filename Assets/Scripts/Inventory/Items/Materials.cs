using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MaterialType { Wood, Stone, Iron, Gold, String, Herbs, RedDust, BlueDust, YellowDust, OrangeDust }

[CreateAssetMenu(fileName = "NewMaterial", menuName = "Items/Materials" )]
public class Materials : Item
{
    public MaterialType materialType;
    public int amountStacked;
    public int valueBought;
    public int valueSold;
}
