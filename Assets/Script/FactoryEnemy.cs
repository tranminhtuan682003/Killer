using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryEnemy : MonoBehaviour
{
    public static FactoryEnemy Instance {  get; private set; }
    public RockEnemy rockEnemyPrefab;
    [SerializeField] private int amountEnemy;
    private ObjectPool<RockEnemy> rockEnemyPool;
    public Transform[] spawnPoints;
    public Transform enemyParrent;
    private GameObject player;

    void Awake()
    {
        rockEnemyPool = new ObjectPool<RockEnemy>(rockEnemyPrefab, amountEnemy,enemyParrent);
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        SetParrentSpawnPoint();
    }
    private void SetParrentSpawnPoint()
    {
        foreach (var spawn in spawnPoints)
        {
            spawn.SetParent(player.transform);
        }
    }
    public IEnemy CreateBoss(string bossType)
    {
        switch (bossType)
        {
            case "RockEnemy":
                return rockEnemyPool.GetAtRandomPosition(spawnPoints);
            default:
                return null;
        }
    }

    public void ReleaseBoss(IEnemy boss)
    {
        if (boss is RockEnemy rockEnemy)
        {
            rockEnemyPool.Release(rockEnemy);
        }
    }
}
