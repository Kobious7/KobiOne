using System;
using UnityEngine;

public class NewAndLockEquip : GMono
{
    private static NewAndLockEquip instance;

    public static NewAndLockEquip Instance => instance;

    public event Action<InventoryEquip, bool> OnNewOrLockChanged;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.LogError("Only have 1 NewAndLockEquip instance");

        instance = this;
    }

    public void OnNewOrLockChangedEventInvoke(InventoryEquip equip, bool inv)
    {
        OnNewOrLockChanged?.Invoke(equip, inv);
    }
}