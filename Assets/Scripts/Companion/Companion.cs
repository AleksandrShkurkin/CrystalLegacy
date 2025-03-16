using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion : LivingEntity
{
    private int previousLevel = -1;

    void Start()
    {
        Level = 0;
        Health = 0;
        Defense = 0;
    }

    void Update()
    {
        Level = Player.Instance.Level;
        if (Level != previousLevel)
        {
            previousLevel = Level;
            AttackDamage = 7 + (previousLevel * 2);
        }
    }

    public override void RecieveDamage(int damage) {}
}
