using UnityEngine;
using TMPro;

public class Player : LivingEntity
{
    public TextMeshProUGUI healthText;

    private float lastDamageTime = -999f;
    private float damageCooldown = 0.8f;

    public void Start()
    {
        health = 100;
        defense = 1.0f;
        attackDamage = 0;
    }

    void Update()
    {
        healthText.text = "HP: " + health.ToString();
    }

    public override void RecieveDamage(int damage)
    {
        if (Time.time - lastDamageTime < damageCooldown)
            return;

        lastDamageTime = Time.time;

        health -= Mathf.RoundToInt(damage / defense);
        if (health <= 0)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
