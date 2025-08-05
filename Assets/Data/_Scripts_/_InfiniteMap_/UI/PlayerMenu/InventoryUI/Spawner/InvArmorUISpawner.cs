using System.Collections.Generic;
using UnityEngine;

public class InvArmorUISpawner : InventoryItemUISpawner
{
    private static InvArmorUISpawner instance;

    public static InvArmorUISpawner Instance => instance;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.LogError("Only have 1 InvArmorUISpawner instance");

        instance = this;
    }

    protected override List<InventoryStuff> GetInventoryItemList()
    {
        return new(InfiniteMapManager.Instance.Inventory.ArmorList);
    }
}