using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCInterractable : MonoBehaviour
{
    public string npcID;
    public bool inRange = false;
    public TextMeshProUGUI npcName;

    public void Start()
    {
        npcName.text = npcID;
    }

    private void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    public void Interact()
    {
        var quest = QuestManager.Instance.GetCurrentQuest();
        if (quest == null) return;

        var task = quest.GetTaskWithID(npcID);
        if (task == null) return;

        if (task.taskType == TaskType.TalkToNPC || task.taskType == TaskType.DeliverItem)
        {
            QuestManager.Instance.CompleteTask(task.taskType, npcID);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}
