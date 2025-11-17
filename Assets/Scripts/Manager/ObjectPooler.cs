using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public GameObject prefab;
        public int size;
        [HideInInspector] public Queue<GameObject> objects = new Queue<GameObject>();
    }

    public List<Pool> pools;

    void Start()
    {
        foreach (var pool in pools)
        {
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                pool.objects.Enqueue(obj);
            }
        }
    }

    public GameObject GetFromPool(GameObject prefab)
    {
        Pool pool = pools.Find(p => p.prefab == prefab);
        if (pool == null)
        {
            Debug.LogWarning("Pool for prefab not found: " + prefab.name);
            return null;
        }

        if (pool.objects.Count == 0)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pool.objects.Enqueue(obj);
        }

        GameObject item = pool.objects.Dequeue();
        pool.objects.Enqueue(item);
        return item;
    }
}
