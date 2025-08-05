using System.Collections.Generic;
using UnityEngine;

public class InvHelmetUISpawner : InventoryItemUISpawner
{
    private static InvHelmetUISpawner instance;

    public static InvHelmetUISpawner Instance => instance;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.LogError("Only have 1 InvHelmetUISpawner instance");

        instance = this;
    }

    protected override List<InventoryStuff> GetInventoryItemList()
    {
        return new(InfiniteMapManager.Instance.Inventory.HelmetList);
    }
}