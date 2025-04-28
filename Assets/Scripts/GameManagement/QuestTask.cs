using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TaskType
{
    TalkToNPC,
    DeliverItem,
    GoToLocation,
    WaitForTime
}

[System.Serializable]
public class QuestTask
{
    public TaskType taskType;
    public string taskDescription;
    public string targetID;
    public MaterialType materialType;
    public int amount = 0;
    public bool isCompleted;

    public List<string> dialogLines;
}
