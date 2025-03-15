using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class Player : LivingEntity
{
    public static Player Instance {get; private set;}

    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI manaText;
    [SerializeField] private int exp = 0;
    public float MoveSpeed { get; set; } = 4f;
    public int Mana { get; set; } = 100;
    private int killCount = 0;
    public GameObject companion;
    public List<Weapon> weapons;

    private void OnEnable()
    {
        Enemy.OnEnemyDefeated += GainExp;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyDefeated -= GainExp;
    }

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Level = 0;
        Health = 100 + (int)(100 * (Level / 10.0f));
        Defense = 1.0f + (Level / 10.0f);
        AttackDamage = 5 + (Level * 2);
    }
    void Update()
    {
        healthText.text = "HP: " + Health.ToString() + "%";
        levelText.text = "Level: " + Level.ToString() + " (" + exp + "/" + (100 + (Level * 50)).ToString() + ")";
        manaText.text = "Mana: " + Mana.ToString() + "/100";
        if (exp >= 100 + (Level * 50))
        {
            LevelUp();
            exp = 0;
        }
    }

    private void LevelUp()
    {
        AchievementManager.Instance.UnlockAchievement("Nothing is clear, but it's very interesting");
        Level += 1;
        Health = 100 + (int)(100 * (Level / 10.0f));
        Defense = 1.0f + (Level / 10.0f);
        AttackDamage = 5 + (Level * 2);
    }

    private void GainExp(int expGain)
    {
        if (Level == 5)
        {
            return;
        }
        exp += expGain;
        Mana += 10;
        killCount++;

        if (killCount == 1)
        {
            AchievementManager.Instance.UnlockAchievement("First Blood");
        }
        if (killCount == 10)
        {
            AchievementManager.Instance.UnlockAchievement("Moral superiority");
            Instantiate(companion, transform.position + (Vector3)(Random.insideUnitCircle.normalized * 3f), Quaternion.identity);
        }
        if (killCount == 50)
        {
            AchievementManager.Instance.UnlockAchievement("Battle Veteran");
        }
        if (Health < 10)
        {
            AchievementManager.Instance.UnlockAchievement("Dead or Alive?");
        }
    }

    public override void RecieveDamage(int damage)
    {
        Health -= Mathf.RoundToInt(damage / Defense);
        if (Health <= 0)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
