using System;
using UnityEngine;

[Serializable]
public class InventoryEquip
{
    [SerializeField] private EquipSO equipSO;

    public EquipSO EquipSO
    {
        get => equipSO;
        set => equipSO = value;
    }

    public InventoryEquip(EquipSO equipSO)
    {
        this.EquipSO = equipSO;
    }
}