using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RewardType
{
    None,
    Material,
    Companion,
    Money
}

[System.Serializable]
public class QuestReward
{
    public RewardType rewardType;

    public MaterialType materialType;
    public int amount = 0;

    public CompanionData companion;
}
