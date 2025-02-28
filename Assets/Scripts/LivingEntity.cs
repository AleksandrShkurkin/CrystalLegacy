using UnityEngine;

public abstract class LivingEntity : MonoBehaviour
{
    public int health;
    public float defense;
    public int attackDamage;

    public abstract void RecieveDamage(int damage);
}
