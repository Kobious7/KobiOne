using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : GMono
{
    [SerializeField] protected List<Transform> prefabs;

    public List<Transform> Prefabs => prefabs;

    [SerializeField] protected Transform holder;

    public Transform Holder => holder;

    [SerializeField] protected List<Transform> objPool;
    [SerializeField] protected int maxSpawnCount;
    public int MaxSpawnCount => maxSpawnCount;
    [SerializeField] protected int spawnCount = 0;

    public int SpawnCount
    {
        get => spawnCount;
        set => spawnCount = value;
    }

    [SerializeField] protected int activeCount;

    public int ActiveCount => activeCount;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadHolder();
        LoadPrefabs();
    }

    protected virtual void LoadHolder()
    {
        if(holder != null) return;

        holder = transform.Find("Holder");
    }

    protected void LoadPrefabs()
    {
        if(prefabs != null && prefabs.Count > 0) return;

        prefabs = new();

        Transform prefabObjs = transform.Find("Prefabs");

        foreach(Transform prefabObj in prefabObjs)
        {
            prefabs.Add(prefabObj);
        }

        HidePrefabs();
    }

    private void HidePrefabs()
    {
        foreach(Transform prefabObj in prefabs)
        {
            prefabObj.gameObject.SetActive(false);
        }
    }

    public Transform Spawn(Transform prefab, Vector3 pos, Quaternion rot)
    {
        Transform obj = GetPrefabByName(prefab);

        if(obj == null) Debug.LogError("Cannot find prefab name: " + prefab.name);

        Transform newObj = GetObjFromPool(obj);

        newObj.SetPositionAndRotation(pos, rot);

        activeCount++;

        return newObj;
    }

    private Transform GetPrefabByName(Transform prefab)
    {
        foreach(Transform obj in prefabs)
        {
            if(prefab.name == obj.name) return prefab;
        }

        return null;
    }

    private Transform GetObjFromPool(Transform obj)
    {
        foreach(Transform poolObj in objPool)
        {
            if(poolObj == null) continue;

            if(poolObj.name == obj.name)
            {
                objPool.Remove(poolObj);
                return poolObj;
            }
        }

        Transform newObj = Instantiate(obj);
        newObj.name = obj.name;

        newObj.SetParent(holder);

        return newObj;
    }

    public virtual void Despawn(Transform obj)
    {
        if (objPool.Contains(obj)) return;
        objPool.Add(obj);
        obj.gameObject.SetActive(false);
        activeCount = activeCount-- <= 0 ? 0 : activeCount--;
    }
}