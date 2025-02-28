using UnityEngine;
using TMPro;

public class Player : LivingEntity
{
    public TextMeshProUGUI healthText;
    public int exp;
    private int killCount = 0;
    public GameObject companion;

    private void OnEnable()
    {
        Enemy.OnEnemyDefeated += GainExp;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyDefeated -= GainExp;
    }

    public void Start()
    {
        level = 0;
        health = 100 + (int)(100 * (level / 10.0f));
        defense = 1.0f + (level / 10.0f);
        attackDamage = 5 + (level * 2);
    }
    void Update()
    {
        healthText.text = "HP: " + health.ToString() + "\nLevel: " + level.ToString() +
        "\nExp: " + exp.ToString() + "/" + (100 + (level * 50)).ToString();
        if (exp >= 100 + (level * 50))
        {
            LevelUp();
            exp = 0;
        }
    }

    private void LevelUp()
    {
        AchievementManager.Instance.UnlockAchievement("Nothing is clear, but it's very interesting");
        level += 1;
        health = 100 + (int)(100 * (level / 10.0f));
        defense = 1.0f + (level / 10.0f);
        attackDamage = 5 + (level * 2);
    }

    private void GainExp(int expGain)
    {
        if (level == 5)
        {
            return;
        }
        exp += expGain;
        killCount++;

        if (killCount == 1)
        {
            AchievementManager.Instance.UnlockAchievement("First Blood");
        }
        if (killCount == 1)
        {
            AchievementManager.Instance.UnlockAchievement("Moral superiority");
            GameObject comp = Instantiate(companion, transform.position + (Vector3)(Random.insideUnitCircle.normalized * 3f), Quaternion.identity);
        }
        if (killCount == 50)
        {
            AchievementManager.Instance.UnlockAchievement("Battle Veteran");
        }
        if (health < 10)
        {
            AchievementManager.Instance.UnlockAchievement("Dead or Alive?");
        }
    }

    public override void RecieveDamage(int damage)
    {
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
