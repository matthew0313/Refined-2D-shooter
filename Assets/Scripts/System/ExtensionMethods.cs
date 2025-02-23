using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class ExtensionMethods
{
    private static Dictionary<GameObject, PooledPrefab> motherPrefabs = new Dictionary<GameObject, PooledPrefab>();
    public static GameObject Create(this GameObject pooledPrefab, Vector2 pos, Quaternion rot)
    {
        if (motherPrefabs.ContainsKey(pooledPrefab))
        {
            return motherPrefabs[pooledPrefab].Create(pos, rot);
        }
        else
        {
            PooledPrefab tmp = MonoBehaviour.Instantiate(pooledPrefab, new Vector2(-10000.0f, 0.0f), Quaternion.identity).GetComponent<PooledPrefab>();
            tmp.OnMotherCreate();
            tmp.gameObject.SetActive(false);
            motherPrefabs.Add(pooledPrefab, tmp);
            return tmp.Create(pos, rot);
        }
    }
    public static GameObject Create(this GameObject pooledPrefab, Vector2 pos, Quaternion rot, Transform parent)
    {
        if (motherPrefabs.ContainsKey(pooledPrefab))
        {
            return motherPrefabs[pooledPrefab].Create(pos, rot, parent);
        }
        else
        {
            PooledPrefab tmp = MonoBehaviour.Instantiate(pooledPrefab, new Vector2(-10000.0f, 0.0f), Quaternion.identity).GetComponent<PooledPrefab>();
            tmp.OnMotherCreate();
            tmp.gameObject.SetActive(false);
            motherPrefabs.Add(pooledPrefab, tmp);
            return tmp.Create(pos, rot, parent);
        }
    }
}
