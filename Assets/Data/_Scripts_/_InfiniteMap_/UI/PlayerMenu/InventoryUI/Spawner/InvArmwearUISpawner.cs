using System.Collections.Generic;
using UnityEngine;

public class InvArmwearUISpawner : InventoryItemUISpawner
{
    private static InvArmwearUISpawner instance;

    public static InvArmwearUISpawner Instance => instance;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.LogError("Only have 1 InvArmwearUISpawner instance");

        instance = this;
    }

    protected override List<InventoryStuff> GetInventoryItemList()
    {
        return new(InfiniteMapManager.Instance.Inventory.ArmwearList);
    }
}