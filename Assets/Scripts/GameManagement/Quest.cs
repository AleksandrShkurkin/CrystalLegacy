using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class Quest
{
    public string questID;
    public string questName;
    public List<QuestTask> tasks;
    public bool isSequential;
    public bool isCompleted;

    public QuestReward reward;

    public bool IsAllTasksCompleted()
    {
        return tasks.All(task => task.isCompleted);
    }

    public QuestTask GetNextTask()
    {
        return tasks.FirstOrDefault(task => !task.isCompleted);
    }

    public QuestTask GetTaskWithID(string id)
    {
        return tasks.FirstOrDefault(task => !task.isCompleted && task.targetID == id);
    }
}
