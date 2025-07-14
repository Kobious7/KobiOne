using System;
using System.Collections.Generic;
using UnityEngine;

public class EquipDisarming : EquipmentAb
{
    public event Action<InventoryEquip> OnEquipDisarming;

    private InfiniteMapManager infiniteMapManager;

    protected override void Start()
    {
        base.Start();
        infiniteMapManager = InfiniteMapManager.Instance;
    }

    public void Unequip(InventoryEquip equip)
    {
        List<InventoryEquip> equipList = infiniteMapManager.Inventory.GetEquipListByEquipType(equip.EquipSO.EquipType);

        if (equip.EquipSO.EquipType == EquipType.Weapon)
        {
            equipList.Add(equip);
            Equipment.Weapon = new InventoryEquip();
            Equipment.Calculator.NewWeaponBonus();
            OnEquipDisarming?.Invoke(equip);
        }
        if (equip.EquipSO.EquipType == EquipType.Helmet)
        {
            equipList.Add(equip);
            Equipment.Helmet = new InventoryEquip();
            Equipment.Calculator.NewHelmetBonus();
            OnEquipDisarming?.Invoke(equip);
        }
        if (equip.EquipSO.EquipType == EquipType.Armor)
        {
            equipList.Add(equip);
            Equipment.Armor = new InventoryEquip();
            Equipment.Calculator.NewArmorBonus();
            OnEquipDisarming?.Invoke(equip);
        }
        if (equip.EquipSO.EquipType == EquipType.Armwear)
        {
            equipList.Add(equip);
            Equipment.Armwear = new InventoryEquip();
            Equipment.Calculator.NewArmwearBonus();
            OnEquipDisarming?.Invoke(equip);
        }
        if (equip.EquipSO.EquipType == EquipType.Boots)
        {
            equipList.Add(equip);
            Equipment.Boots = new InventoryEquip();
            Equipment.Calculator.NewBootsBonus();
            OnEquipDisarming?.Invoke(equip);
        }
        if (equip.EquipSO.EquipType == EquipType.Special)
        {
            equipList.Add(equip);
            Equipment.Special = new InventoryEquip();
            Equipment.Calculator.NewSpecialBonus();
            OnEquipDisarming?.Invoke(equip);
        }

        infiniteMapManager.Equipment.Calculator.CalculateTotalBonus();
        infiniteMapManager.Player.StatsSystem.UpdateEquipBonus(infiniteMapManager.Equipment.StatsBonus);
    }

    public void DisarmInvoke(InventoryEquip equip)
    {
        OnEquipDisarming?.Invoke(equip);
    }
}