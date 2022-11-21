using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolItem
{
    public GameObject pooledObject;
    public int pooledAmount;
    public bool canExpand = true;
}

public class ObjectPooling : MonoBehaviour
{
    public List<PoolItem> itemsToPool;
    public List<GameObject> pooledObjects;

    #region Singleton
    public static ObjectPooling instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    #endregion

    private void Start()
    {
        pooledObjects = new List<GameObject>();

        foreach(PoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.pooledAmount; i++)
            {
                GameObject obj = Instantiate(item.pooledObject);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
    }

    public GameObject GetPooledObjects(string tag)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
                return pooledObjects[i];
        }

        foreach(PoolItem item in itemsToPool)
        {
            if(item.pooledObject.tag == tag)
                if (item.canExpand)
                {
                    GameObject obj = (GameObject)Instantiate(item.pooledObject);
                    obj.SetActive(false);

                    pooledObjects.Add(obj);

                    return obj;
                }
        }
        
        return null;
    }
}
