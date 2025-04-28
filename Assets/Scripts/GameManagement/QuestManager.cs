using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    public List<Quest> allQuests;
    private int currentQuestIndex = 0;
    public TextMeshProUGUI questText;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        if (allQuests.Count > 0)
        {
            UpdateText();
        }
    }

    public Quest GetCurrentQuest()
    {
        if (currentQuestIndex >= allQuests.Count) return null;
        return allQuests[currentQuestIndex];
    }

    public void CompleteTask(TaskType taskType, string targetID)
    {
        var quest = GetCurrentQuest();
        if (quest == null || quest.isCompleted) return;

        if (quest.isSequential)
        {
            var nextTask = quest.GetNextTask();
            if (nextTask != null && nextTask.taskType == taskType && nextTask.targetID == targetID)
            {
                if (nextTask.taskType == TaskType.DeliverItem)
                {
                    if (Inventory.Instance.CheckAmount(nextTask.materialType, nextTask.amount))
                    {
                        Inventory.Instance.RemoveMaterial(nextTask.materialType, nextTask.amount);
                    }
                    else
                    {
                        Debug.Log("Not enough items");
                        return;
                    }
                }
                DialogManager.Instance.StartDialog(nextTask.dialogLines.ToArray());
                nextTask.isCompleted = true;
                Debug.Log($"Task {nextTask.taskDescription} completed.");
            }
            else
            {
                return;
            }
        }
        else
        {
            var task = quest.tasks.FirstOrDefault(t => t.taskType == taskType && t.targetID == targetID);
            if (task != null)
            {
                if (task.taskType == TaskType.DeliverItem)
                {
                    if (Inventory.Instance.CheckAmount(task.materialType, task.amount))
                    {
                        Inventory.Instance.RemoveMaterial(task.materialType, task.amount);
                    }
                    else
                    {
                        Debug.Log("Not enough items");
                        return;
                    }
                }
                DialogManager.Instance.StartDialog(task.dialogLines.ToArray());
                task.isCompleted = true;
                Debug.Log($"Task {task.taskDescription} completed.");
            }
        }

        if (quest.IsAllTasksCompleted()) {
            quest.isCompleted = true;
            GiveReward(quest);
            currentQuestIndex++;
            Debug.Log($"Quest {quest.questName} completed!");
        }

        UpdateText();
    }

    private void UpdateText()
    {
        var quest = GetCurrentQuest();
        if (quest == null) 
        {
            questText.text = "No active quest";
            return;
        }
        if (quest.isSequential)
        {
            var task = quest.GetNextTask();
            questText.text = $"Quest: {quest.questName}\n\nTask: {task.taskDescription}";
        }
        else
        {
            questText.text = $"Quest: {quest.questName}\n\nTasks: {string.Join("\n", quest.tasks.Select(t => $"{t.taskDescription} - {(t.isCompleted ? "Completed" : "Not completed")}"))}";
        }
    }

    private void GiveReward(Quest quest)
    {
        if (quest.reward == null || quest.reward.rewardType == RewardType.None) return;

        switch (quest.reward.rewardType)
        {
            case RewardType.Material:
                Inventory.Instance.AddMaterial(quest.reward.materialType, quest.reward.amount);
                break;
            case RewardType.Companion:
                Inventory.Instance.unlockedCompanions.Add(quest.reward.companion);
                break;
            case RewardType.Money:
                Player.Instance.Money += quest.reward.amount;
                break;
        }
    }
}
