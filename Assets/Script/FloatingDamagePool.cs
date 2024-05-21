using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingDamagePool
{
    private List<GameObject> pool = new List<GameObject>();
    private GameObject prefab;

    public FloatingDamagePool(GameObject prefab, int initialSize, Transform UIDamageParent)
    {
        this.prefab = prefab;
        for (int i = 0; i < initialSize; i++)
        {
            GameObject obj = GameObject.Instantiate(prefab, UIDamageParent);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject Get()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }
        GameObject newObj = GameObject.Instantiate(prefab, prefab.transform.parent);
        newObj.SetActive(false);
        pool.Add(newObj);
        return newObj;
    }
}
