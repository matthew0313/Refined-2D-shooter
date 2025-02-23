using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class PooledPrefab : MonoBehaviour
{
    bool clone = false;
    List<PooledPrefab> pool;
    public void OnMotherCreate()
    {
        pool = new List<PooledPrefab>();
    }
    public virtual void OnCreate()
    {
        clone = true;
    }
    public GameObject Create(Vector3 position, Quaternion rotation)
    {
        GameObject tmp;
        Debug.Log(pool == null);
        while (pool.Count > 0 && pool[0] == null) pool.RemoveAt(0);
        if(pool.Count > 0)
        {
            pool[0].OnCreate();
            tmp = pool[0].gameObject;
            pool.RemoveAt(0);
        }
        else
        {
            tmp = Instantiate(gameObject);
            PooledPrefab a = tmp.GetComponent<PooledPrefab>();
            a.OnCreate();
            a.pool = pool;
        }
        tmp.SetActive(true);
        tmp.transform.position = position;
        tmp.transform.rotation = rotation;
        return tmp;
    }
    public GameObject Create(Vector3 position, Quaternion rotation, Transform parent)
    {
        GameObject tmp;
        Debug.Log(pool == null);
        while (pool.Count > 0 && pool[0] == null) pool.RemoveAt(0);
        if (pool.Count > 0)
        {
            pool[0].OnCreate();
            tmp = pool[0].gameObject;
            pool.RemoveAt(0);
        }
        else
        {
            tmp = Instantiate(gameObject, parent, false);
            PooledPrefab a = tmp.GetComponent<PooledPrefab>();
            a.OnCreate();
            a.pool = pool;
        }
        tmp.SetActive(true);
        tmp.transform.localPosition = position;
        tmp.transform.localRotation = rotation;
        return tmp;
    }
    protected virtual void UponDestroy()
    {

    }
    protected void Destroy()
    {
        if (gameObject.activeSelf == false) return;
        UponDestroy();
        gameObject.SetActive(false);
        pool.Add(this);
    }
    public void Disconnect()
    {
        
    }
}
