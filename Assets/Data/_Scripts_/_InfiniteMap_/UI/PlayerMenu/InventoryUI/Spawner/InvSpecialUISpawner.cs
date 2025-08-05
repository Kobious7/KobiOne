using System.Collections.Generic;
using UnityEngine;

public class InvSpecialUISpawner : InventoryItemUISpawner
{
    private static InvSpecialUISpawner instance;

    public static InvSpecialUISpawner Instance => instance;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.LogError("Only have 1 InvSpecialUISpawner instance");

        instance = this;
    }

    protected override List<InventoryStuff> GetInventoryItemList()
    {
        return new(InfiniteMapManager.Instance.Inventory.SpecialList);
    }
}