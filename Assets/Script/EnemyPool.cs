using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform enemyParent;
    public int maxEnemyCount = 20;
    public float spawnIntervalMin = 1f;
    public float spawnIntervalMax = 3f;

    private List<GameObject> activeEnemies = new List<GameObject>();
    private int enemyIndex = 0;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (enemyIndex < maxEnemyCount)
        {
            GameObject enemy = Instantiate(enemyPrefab, enemyParent);
            enemy.SetActive(true);
            activeEnemies.Add(enemy);
            enemyIndex++;
            yield return new WaitForSeconds(Random.Range(spawnIntervalMin, spawnIntervalMax));
        }
    }
}
