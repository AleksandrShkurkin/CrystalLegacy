using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    private void Start()
    {
        var spawnPoints = FindObjectsOfType<SpawnPoint>();
        
        var spawnPointID = PlayerPrefs.GetString("SpawnPointID");
        
        var targetSpawnPoint = System.Array.Find(spawnPoints, x => x.spawnPointID == spawnPointID);

        if (!targetSpawnPoint) return;
        Player.Player.Instance.transform.position = targetSpawnPoint.transform.position;
        Player.Player.Instance.transform.rotation = targetSpawnPoint.transform.rotation;
        
        var companion = GameObject.FindGameObjectWithTag("Companion");
        if (!companion) return;
        companion.transform.position = targetSpawnPoint.transform.position;
        companion.transform.rotation = targetSpawnPoint.transform.rotation;
    }
}
