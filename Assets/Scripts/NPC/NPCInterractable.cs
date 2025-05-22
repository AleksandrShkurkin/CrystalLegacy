using TMPro;
using UnityEngine;

namespace NPC
{
    // ReSharper disable once InconsistentNaming
    public class NPCInterractable : MonoBehaviour
    {
        public string npcID;
        public bool inRange;
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

        private void Interact()
        {
            var quest = QuestManager.Instance.GetCurrentQuest();

            var task = quest?.GetTaskWithID(npcID);

            if (task?.taskType is TaskType.TalkToNPC or TaskType.DeliverItem)
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
}
