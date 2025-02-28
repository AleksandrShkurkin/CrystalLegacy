using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion : LivingEntity
{
    private Player player;
    private int previousLevel = -1;

    void Start()
    {
        level = 0;
        health = 0;
        defense = 0;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        level = player.level;
        if (level != previousLevel)
        {
            previousLevel = level;
            attackDamage = 7 + (previousLevel * 2);
        }
    }

    public override void RecieveDamage(int damage) {}
}
