using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BosManager : MonoBehaviour
{
    public FactoryEnemy factoryEnemy;
    private float spawnInterval = 1f;
    private float intervalDecreaseRate = 0.1f;
    private float minSpawnInterval = 0.1f;

    void Start()
    {
        StartCoroutine(AdjustSpawnRate());
        InvokeRepeating("SpawnBoss", 0f, spawnInterval);
    }

    private void SpawnBoss()
    {
        string bossType = Random.Range(0, 2) == 0 ? "RockEnemy" : "IceBoss";
        IEnemy boss = factoryEnemy.CreateBoss(bossType);
    }
    IEnumerator AdjustSpawnRate()
    {
        while (spawnInterval > minSpawnInterval)
        {
            yield return new WaitForSeconds(10f);
            spawnInterval = Mathf.Max(spawnInterval - intervalDecreaseRate, minSpawnInterval);
            CancelInvoke("SpawnBoss");
            InvokeRepeating("SpawnBoss", 0f, spawnInterval);
        }
    }
    private void OnDestroy()
    {
        CancelInvoke("SpawnBoss");
        StopAllCoroutines();
    }
}
