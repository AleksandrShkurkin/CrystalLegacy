
using UnityEngine;
using TMPro;
using System;

public class Enemy : LivingEntity
{
    [SerializeField] private TextMeshProUGUI healthText;
    public static event Action<int> OnEnemyDefeated;
    public event Action OnDeathRespawn;

    public void Start()
    {
        Level = Math.Clamp(UnityEngine.Random.Range(Player.Player.Instance.Level - 1, Player.Player.Instance.Level + 1), 0, 5);
        Health = 100 + (int)(100 * (Level / 10.0f));
        Defense = 1.0f + (Level / 10.0f);
        AttackDamage = 10 + (Level * 2);
    }

    void Update()
    {
        healthText.text = "HP: " + Health.ToString() + "\nLevel: " + Level.ToString();
    }

    public override void RecieveDamage(int damage)
    {
        Health -= Mathf.RoundToInt(damage / Defense);
        if (Health <= 0)
        {
            OnDeathRespawn?.Invoke();
            OnEnemyDefeated?.Invoke(Math.Max(Level * 10, 5));
            Destroy(gameObject);
        }
    }
}
