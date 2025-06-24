using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PrefabSpawner : GMono
{
    [SerializeField] protected List<Transform> prefabs;

    public List<Transform> Prefabs => prefabs;

    [SerializeField] protected Transform holder;

    public Transform Holder => holder;

    [SerializeField] protected List<Transform> objPool;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadHolder();
    }

    protected virtual void LoadHolder()
    {
        if(holder != null) return;

        holder = transform.Find("Holder");
    }

    public Transform Spawn(Transform prefab, Vector3 pos, Quaternion rot)
    {
        Transform newObj = GetObjFromPool(prefab);

        newObj.SetPositionAndRotation(pos, rot);

        return newObj;
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
    }
}