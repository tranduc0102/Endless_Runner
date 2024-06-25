using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public List<Transform> poolObject;
    public Transform holder;

    private void Awake()
    {
        this.LoadHolder("Holder");
    }

    protected void LoadHolder(String s)
    {
        if(holder!=null)return;
        holder = transform.Find(s);
    }

    public Transform Spawner(Transform gameObject, Vector3 positionSpawn, Quaternion rotation)
    {
        Transform newPrefab = this.GetObjFromPool(gameObject);
        newPrefab.SetPositionAndRotation(positionSpawn,rotation);
        newPrefab.parent = this.holder;
        return newPrefab;
    }

    public GameObject Spawner(GameObject gameObject, Vector3 positionSpawn, Quaternion rotation)
    {
        Transform newPrefab = this.GetObjFromPool(gameObject.transform);
        newPrefab.SetPositionAndRotation(positionSpawn,rotation);
        newPrefab.gameObject.SetActive(true);
        newPrefab.parent = this.holder;
        return newPrefab.gameObject;
    }

    protected Transform GetObjFromPool(Transform prefab)
    {
        foreach (Transform obj in poolObject)
        {
            if (prefab.name == obj.name)
            {
                this.poolObject.Remove(obj);
                prefab = obj;
                prefab.gameObject.SetActive(true);
                return prefab;
            }
        }
        Transform newPrefab = Instantiate(prefab);
        newPrefab.name = prefab.name;
        newPrefab.gameObject.SetActive(true);
        newPrefab.parent = this.holder;
        return newPrefab;
    }

    public void Despawn(Transform obj)
    {
        this.poolObject.Add(obj);
        obj.gameObject.SetActive(false);
    }
    public void Despawn(GameObject obj)
    {
        this.poolObject.Add(obj.transform);
        obj.gameObject.SetActive(false);
    }
}
