using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class Player : LivingEntity
{
    public static Player Instance {get; private set;}

    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI manaText;
    [SerializeField] private TextMeshProUGUI moneyText;
    public int Exp { get; private set; } = 0;
    public float MoveSpeed { get; set; } = 4f;
    public int Mana { get; set; } = 100;
    public int Money { get; set; } = 100;
    private int killCount = 0;
    public CompanionData companion1;
    public CompanionData companion2;
    public CompanionData companion3;
    public CompanionData companion4;
    public List<Weapon> weapons;
    public EffectType? activeEffectType = null;
    public int activeEffectValue = 0;

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
        levelText.text = "Level: " + Level.ToString() + " (" + Exp + "/" + (100 + (Level * 50)).ToString() + ")";
        manaText.text = "Mana: " + Mana.ToString() + "/100";
        moneyText.text = "Money: " + Money.ToString() + "$";
        if (Exp >= 100 + (Level * 50))
        {
            LevelUp();
            Exp = 0;
        }
    }

    private void LevelUp()
    {
        AchievementManager.Instance.UnlockAchievement("Nothing is clear, but it's very interesting");
        if (Level == 0)
        {
            Inventory.Instance.unlockedCompanions.Add(companion3);
        }
        Level += 1;
        Health = 100 + (int)(100 * (Level / 10.0f));
        Defense = 1.0f + (Level / 10.0f);
        AttackDamage = 5 + (Level * 2);
    }

    private void GainExp(int ExpGain)
    {
        if (Level == 5)
        {
            return;
        }
        Exp += ExpGain;
        if (Mana + 10 <= 100)
            Mana += 10;
        killCount++;

        if (killCount == 1)
        {
            AchievementManager.Instance.UnlockAchievement("First Blood");
            Inventory.Instance.unlockedCompanions.Add(companion1);
        }
        if (killCount == 10)
        {
            AchievementManager.Instance.UnlockAchievement("Moral superiority");
            Inventory.Instance.unlockedCompanions.Add(companion2);
            
        }
        if (killCount == 50)
        {
            AchievementManager.Instance.UnlockAchievement("Battle Veteran");
        }
        if (Health < 10)
        {
            AchievementManager.Instance.UnlockAchievement("Dead or Alive?");
            Inventory.Instance.unlockedCompanions.Add(companion4);
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

    public void ApplyCompanionEffect(CompanionData companionData)
    {
        activeEffectType = companionData.effectType;
        activeEffectValue = companionData.effectValue;
    }

    public void ResetCompanionEffect()
    {
        activeEffectType = null;
        activeEffectValue = 0;
    }
}
