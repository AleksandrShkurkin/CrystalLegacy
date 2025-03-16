using UnityEngine;

public abstract class LivingEntity : MonoBehaviour
{
    public int Health {get; set;}
    public float Defense {get; set;}
    public int AttackDamage {get; set;}
    public int Level {get; protected set;}

    public abstract void RecieveDamage(int damage);
}
