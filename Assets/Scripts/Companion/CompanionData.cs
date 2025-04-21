using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType { None, Health, Mana, Damage }

[CreateAssetMenu(fileName = "CompanionData", menuName = "Companion/New Companion")]
public class CompanionData : ScriptableObject
{
    public string companionName;
    public Sprite icon;
    public GameObject companionPrefab;
    public EffectType effectType;
    public int effectValue;
}
