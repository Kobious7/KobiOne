using System.Collections.Generic;
using UnityEngine;

public class InvWeaponUISpawner : InventoryItemUISpawner
{
    private static InvWeaponUISpawner instance;

    public static InvWeaponUISpawner Instance => instance;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.LogError("Only have 1 InvWeaponUISpawner instance");

        instance = this;
    }

    protected override List<InventoryStuff> GetInventoryItemList()
    {
        return new(InfiniteMapManager.Instance.Inventory.WeaponList);
    }
}