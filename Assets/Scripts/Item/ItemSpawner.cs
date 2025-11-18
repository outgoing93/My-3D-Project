using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject plusItemPrefab;
    [SerializeField] private GameObject minusItemPrefab;
    [SerializeField] private GameObject bombItemPrefab;

    public float spawnInterval = 2f; 
    public Vector3 spawnAreaMin;
    public Vector3 spawnAreaMax;

    private ObjectPooler pooler;

    private void Start()
    {
        pooler = GetComponent<ObjectPooler>();
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnRandomItem();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnRandomItem()
    {
        int rand = Random.Range(0, 3);
        GameObject prefab = null;
        ItemType type = ItemType.Plus;

        switch (rand)
        {
            case 0:
                prefab = plusItemPrefab;
                type = ItemType.Plus;
                break;
            case 1:
                prefab = minusItemPrefab;
                type = ItemType.Minus;
                break;
            case 2:
                prefab = bombItemPrefab;
                type = ItemType.Bomb;
                break;
        }

        Vector3 pos = new Vector3(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y),
            Random.Range(spawnAreaMin.z, spawnAreaMax.z)
        );

        GameObject item = pooler.GetFromPool(prefab, type);
        if (item != null)
        {
            item.transform.position = pos;
            item.SetActive(true);
        }
    }
}
