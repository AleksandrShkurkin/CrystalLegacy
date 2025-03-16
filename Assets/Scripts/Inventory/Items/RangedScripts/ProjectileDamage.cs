using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    private Collider2D hitbox;
    private int bulletDamage;

    void Start()
    {
        hitbox = GetComponent<Collider2D>();
    }

    public void SetDamage(int dmg)
    {
        bulletDamage = dmg;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            Destroy(gameObject);
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            other.GetComponent<Enemy>().RecieveDamage(bulletDamage);
            Destroy(gameObject);
        }
    }
}
