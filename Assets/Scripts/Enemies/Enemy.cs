using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : LivingEntity
{
    public TextMeshProUGUI healthText;
    public void Start()
    {
        health = 100;
        defense = 1.0f;
        attackDamage = 10;
    }

    void Update()
    {
        healthText.text = "HP: " + health.ToString();
    }

    public override void RecieveDamage(int damage)
    {
        health -= Mathf.RoundToInt(damage / defense);
        if (health <= 0)
        {
            Respawn();
            Destroy(gameObject);
        }
    }

    void Respawn()
    {
        float arenaSizeX = 5.0f;
        float arenaSizeY = 5.0f;
        Vector3 randomPosition = new Vector3(
            Random.Range(-arenaSizeX, arenaSizeX),
            Random.Range(-arenaSizeY, arenaSizeY),
            transform.position.z
        );
        gameObject.GetComponent<Enemy>().health = 100;
        Instantiate(gameObject, randomPosition, Quaternion.identity);
    }
}
