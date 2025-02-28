using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    public GameObject enemyPrefab; 
    public int maxEnemies = 5;
    public float respawnDelay = 3f;
    private List<GameObject> enemies = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(SpawnEnemiesLoop());
    }

    private IEnumerator SpawnEnemiesLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnDelay);

            if (enemies.Count < maxEnemies)
            {
                Vector3 spawnPos = GetRandomSpawnPosition();
                GameObject newEnemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
                enemies.Add(newEnemy);
                newEnemy.GetComponent<Enemy>().OnDeathRespawn += () => RemoveEnemy(newEnemy);
            }
        }
    }

    private void RemoveEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Bounds bounds = GetComponent<Collider2D>().bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector3(x, y, 0);
    }
}
