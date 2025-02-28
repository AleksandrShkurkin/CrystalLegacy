using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance;

    private List<Achievement> achievements = new List<Achievement>();
    private Queue<Achievement> achievementQueue = new Queue<Achievement>();
    private bool isDisplayingAchievement = false;
    public TextMeshProUGUI achievementText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        achievements.Add(new Achievement("Nothing is clear, but it's very interesting",
         "Level up to the first level."));
        achievements.Add(new Achievement("First Blood", "Defeat 1 monster."));
        achievements.Add(new Achievement("Moral superiority", "Get your first companion."));
        achievements.Add(new Achievement("Dead or Alive?", "Survive a battle with less than 10 HP left."));
        achievements.Add(new Achievement("Battle Veteran", "Defeat 50 monsters."));
    }

    public void UnlockAchievement(string achievementName)
    {
        Achievement achievement = achievements.Find(a => a.name == achievementName);

        if (achievement != null && !achievement.isUnlocked)
        {
            achievement.isUnlocked = true;
            achievementQueue.Enqueue(achievement);
            TryShowNextAchievement();
        }
    }

    private void TryShowNextAchievement()
    {
        if (!isDisplayingAchievement && achievementQueue.Count > 0)
        {
            Achievement nextAchievement = achievementQueue.Dequeue();
            StartCoroutine(DisplayAchievement(nextAchievement));
        }
    }

    private IEnumerator DisplayAchievement(Achievement achievement)
    {
        isDisplayingAchievement = true;

        achievementText.text = achievement.name + "\n\n" + achievement.description;

        yield return new WaitForSeconds(3f);

        achievementText.text = "";
        isDisplayingAchievement = false;
        TryShowNextAchievement();
    }
}
