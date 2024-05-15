using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BosManager : MonoBehaviour
{
    public FactoryEnemy factoryEnemy;
    private float spawnInterval = 1f;
    private float intervalDecreaseRate = 0.1f; // Giảm khoảng thời gian sinh boss đi 0.1 giây mỗi 10 giây
    private float minSpawnInterval = 0.1f; // Giới hạn tối thiểu để tránh sinh quá nhanh

    void Start()
    {
        StartCoroutine(AdjustSpawnRate());
        InvokeRepeating("SpawnBoss", 0f, spawnInterval);
    }

    void SpawnBoss()
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

    void OnDestroy()
    {
        CancelInvoke("SpawnBoss");
        StopAllCoroutines(); // Dừng tất cả coroutine khi đối tượng bị phá hủy
    }
}
