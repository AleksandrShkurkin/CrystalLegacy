
using UnityEngine;
using TMPro;
using System;

public class Enemy : LivingEntity
{
    public TextMeshProUGUI healthText;
    public static event Action<int> OnEnemyDefeated;
    public event Action OnDeathRespawn;

    public void Start()
    {
        level = UnityEngine.Random.Range(0, 5);
        health = 100 + (int)(100 * (level / 10.0f));
        defense = 1.0f + (level / 10.0f);
        attackDamage = 10 + (level * 2);
    }

    void Update()
    {
        healthText.text = "HP: " + health.ToString() + "\nLevel: " + level.ToString();
    }

    public override void RecieveDamage(int damage)
    {
        health -= Mathf.RoundToInt(damage / defense);
        if (health <= 0)
        {
            OnDeathRespawn?.Invoke();
            OnEnemyDefeated?.Invoke(Math.Max(level * 10, 5));
            Destroy(gameObject);
        }
    }
}
