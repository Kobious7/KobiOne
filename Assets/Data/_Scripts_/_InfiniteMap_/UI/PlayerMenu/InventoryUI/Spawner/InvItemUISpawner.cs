using System.Collections.Generic;
using UnityEngine;

public class InvItemUISpawner : InventoryItemUISpawner
{
    private static InvItemUISpawner instance;

    public static InvItemUISpawner Instance => instance;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.LogError("Only have 1 InvItemUISpawner instance");

        instance = this;
    }
    
    protected override List<InventoryStuff> GetInventoryItemList()
    {
        return new(InfiniteMapManager.Instance.Inventory.ItemList);
    }
}