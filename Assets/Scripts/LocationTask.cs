using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationTask : MonoBehaviour
{
    public string locationID;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            QuestManager.Instance.CompleteTask(TaskType.GoToLocation, locationID);
        }
    }
}
