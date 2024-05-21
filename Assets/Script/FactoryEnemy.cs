using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryEnemy : MonoBehaviour
{
    public static FactoryEnemy Instance { get; private set; }
    public RockEnemy rockEnemyPrefab;
    [SerializeField] private int amountEnemy;
    private ObjectPool<RockEnemy> rockEnemyPool;
    public Transform[] spawnPoints;
    public Transform enemyParent;
    private GameObject player;

    public GameObject floatingPrefab;
    public GameObject DamagePrefab;
    private FloatingDamagePool damagePool;
    private FloatingDamagePool floatingDamagePool;

    void Awake()
    {
        Instance = this;
        rockEnemyPool = new ObjectPool<RockEnemy>(rockEnemyPrefab, amountEnemy, enemyParent);
        CreatePoolFloating();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        SetParentSpawnPoint();
    }

    private void CreatePoolFloating()
    {
        try
        {
            damagePool = new FloatingDamagePool(DamagePrefab, 100, enemyParent);
            floatingDamagePool = new FloatingDamagePool(floatingPrefab, 100, enemyParent);
        }
        catch (Exception ex)
        {
            Debug.LogError("null : " + ex.Message);
        }
    }

    private void SetParentSpawnPoint()
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

    public void UseFloatingPrefab(Transform bulletPosition, float Time)
    {
        GameObject floatingInstance = floatingDamagePool.Get();
        GameObject damageInstance = damagePool.Get();
        if (floatingInstance != null && damageInstance != null)
        {
            damageInstance.transform.position = new Vector3(bulletPosition.position.x,bulletPosition.position.y,0);
            floatingInstance.transform.position = new Vector3(bulletPosition.position.x + 0.7f, bulletPosition.position.y + 0.7f, 0);

            StartCoroutine(DisActiveFloatingPrefab(damageInstance, floatingInstance, Time));
        }
    }
    IEnumerator DisActiveFloatingPrefab(GameObject damageInstance, GameObject floatingInstance, float time)
    {
        yield return new WaitForSeconds(time);
        damageInstance.SetActive(false);
        floatingInstance.SetActive(false);
    }
}
