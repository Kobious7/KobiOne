using System;
using System.Collections.Generic;
using UnityEngine;

public class EquipDisarming : EquipmentAb
{
    public event Action<InventoryEquip> OnEquipDisarming;

    private InfiniteMapManager infiniteMapManager;
    private PlayerSO playerData;

    protected override void Start()
    {
        base.Start();
        infiniteMapManager = InfiniteMapManager.Instance;
        playerData = infiniteMapManager.PlayerData;
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

            //Save Data
            playerData.Weapon = Equipment.Weapon;
            playerData.WeaponList = equipList;
        }

        if (equip.EquipSO.EquipType == EquipType.Helmet)
        {
            equipList.Add(equip);
            Equipment.Helmet = new InventoryEquip();
            Equipment.Calculator.NewHelmetBonus();
            OnEquipDisarming?.Invoke(equip);

            //Save Data
            playerData.Helmet = Equipment.Helmet;
            playerData.HelmetList = equipList;
        }

        if (equip.EquipSO.EquipType == EquipType.Armor)
        {
            equipList.Add(equip);
            Equipment.Armor = new InventoryEquip();
            Equipment.Calculator.NewArmorBonus();
            OnEquipDisarming?.Invoke(equip);

            //Save Data
            playerData.Armor = Equipment.Armor;
            playerData.ArmorList = equipList;
        }

        if (equip.EquipSO.EquipType == EquipType.Armwear)
        {
            equipList.Add(equip);
            Equipment.Armwear = new InventoryEquip();
            Equipment.Calculator.NewArmwearBonus();
            OnEquipDisarming?.Invoke(equip);

            //Save Data
            playerData.Armwear = Equipment.Armwear;
            playerData.ArmwearList = equipList;
        }

        if (equip.EquipSO.EquipType == EquipType.Boots)
        {
            equipList.Add(equip);
            Equipment.Boots = new InventoryEquip();
            Equipment.Calculator.NewBootsBonus();
            OnEquipDisarming?.Invoke(equip);

            //Save Data
            playerData.Boots = Equipment.Boots;
            playerData.BootsList = equipList;
        }

        if (equip.EquipSO.EquipType == EquipType.Special)
        {
            equipList.Add(equip);
            Equipment.Special = new InventoryEquip();
            Equipment.Calculator.NewSpecialBonus();
            OnEquipDisarming?.Invoke(equip);

            //Save Data
            playerData.Special = Equipment.Special;
            playerData.SpecialList = equipList;
        }

        infiniteMapManager.Equipment.Calculator.CalculateTotalBonus();
    }

    public void DisarmInvoke(InventoryEquip equip)
    {
        OnEquipDisarming?.Invoke(equip);
    }
}