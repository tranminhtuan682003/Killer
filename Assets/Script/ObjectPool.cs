using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private Stack<T> pool = new Stack<T>();
    private T prefab;

    public ObjectPool(T prefab, int initialSize, Transform enemyParrent)
    {
        this.prefab = prefab;
        for (int i = 0; i < initialSize; i++)
        {
            T obj = GameObject.Instantiate(prefab,enemyParrent);
            obj.gameObject.SetActive(false);
            pool.Push(obj);
        }
    }
    public T Get()
    {
        if (pool.Count > 0)
        {
            T obj = pool.Pop();
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            T obj = GameObject.Instantiate(prefab);
            return obj;
        }
    }
    public T GetAtRandomPosition(Transform[] spawnPoints)
    {
        T obj = Get();
        if (spawnPoints.Length > 0)
        {
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            obj.transform.position = randomSpawnPoint.position;
        }
        return obj;
    }
    public void Release(T obj)
    {
        obj.gameObject.SetActive(false);
        pool.Push(obj);
    }
}
