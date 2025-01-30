using UnityEngine;

public class Player : LivingEntity
{
    public void Start()
    {
        health = 100;
        defense = 1.0f;
        attackDamage = 0;
    }
    
    public override void RecieveDamage(int damage)
    {
        health -= Mathf.RoundToInt(damage / defense);
        //TODO: Implement death 
        if (health <= 0)
        {
            health = 1;
        }
    }
}
