using System.Collections.Generic;
using UnityEngine;

public class InvBootsUISpawner : InventoryItemUISpawner
{
    private static InvBootsUISpawner instance;

    public static InvBootsUISpawner Instance => instance;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.LogError("Only have 1 InvBootsUISpawner instance");

        instance = this;
    }

    protected override List<InventoryStuff> GetInventoryItemList()
    {
        return new(InfiniteMapManager.Instance.Inventory.BootsList);
    }
}