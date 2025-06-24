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

        if (equip.EquipSO.EquipType == EquipType.WEAP)
        {
            equipList.Add(equip);
            Equipment.Weapon = new InventoryEquip();
            Equipment.Calculator.NewWeaponBonus();
            OnEquipDisarming?.Invoke(equip);
        }
        if (equip.EquipSO.EquipType == EquipType.HELMET)
        {
            equipList.Add(equip);
            Equipment.Helmet = new InventoryEquip();
            Equipment.Calculator.NewHelmetBonus();
            OnEquipDisarming?.Invoke(equip);
        }
        if (equip.EquipSO.EquipType == EquipType.BODYARMOR)
        {
            equipList.Add(equip);
            Equipment.BodyArmor = new InventoryEquip();
            Equipment.Calculator.NewBodyArmorBonus();
            OnEquipDisarming?.Invoke(equip);
        }
        if (equip.EquipSO.EquipType == EquipType.LEGARMOR)
        {
            equipList.Add(equip);
            Equipment.LegArmor = new InventoryEquip();
            Equipment.Calculator.NewLegArmorBonus();
            OnEquipDisarming?.Invoke(equip);
        }
        if (equip.EquipSO.EquipType == EquipType.BOOTS)
        {
            equipList.Add(equip);
            Equipment.Boots = new InventoryEquip();
            Equipment.Calculator.NewBootsBonus();
            OnEquipDisarming?.Invoke(equip);
        }
        if (equip.EquipSO.EquipType == EquipType.BACKITEM)
        {
            equipList.Add(equip);
            Equipment.BackItem = new InventoryEquip();
            Equipment.Calculator.NewBackItemBonus();
            OnEquipDisarming?.Invoke(equip);
        }
        if (equip.EquipSO.EquipType == EquipType.AURA)
        {
            equipList.Add(equip);
            Equipment.Aura = new InventoryEquip();
            Equipment.Calculator.NewAuraBonus();
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