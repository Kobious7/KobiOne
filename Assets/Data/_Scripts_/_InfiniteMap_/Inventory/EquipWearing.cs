using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipWearing : InventoryAb
{
    public event Action<InventoryEquip> OnEquipWearing;
    private Equipment equipment;
    private PlayerSO playerData;

    protected override void Start()
    {
        base.Start();
        equipment = InfiniteMapManager.Instance.Equipment;
        playerData = InfiniteMapManager.Instance.PlayerData;
    }

    public void Equip(InventoryEquip equip)
    {
        List<InventoryEquip> equipList = Inventory.GetEquipListByEquipType(equip.EquipSO.EquipType);

        if (equip.EquipSO.EquipType == EquipType.Weapon)
        {
            if (equipment.Weapon != null && equipment.Weapon.Level > 0)
            {
                equipList.Add(equipment.Weapon);
                equipment.Calculator.NewWeaponBonus();
                equipment.Unequip.DisarmInvoke(equipment.Weapon);
            }

            equipment.Weapon = equip;
            equipList.Remove(equip);
            equipment.Calculator.CalculateWeapon();
            OnEquipWearing?.Invoke(equipment.Weapon);

            //Save Data
            playerData.Weapon = equipment.Weapon;
            playerData.WeaponList = equipList;
        }
        
        if (equip.EquipSO.EquipType == EquipType.Helmet)
        {
            if (equipment.Helmet != null && equipment.Helmet.Level > 0)
            {
                equipList.Add(equipment.Helmet);
                equipment.Calculator.NewHelmetBonus();
                equipment.Unequip.DisarmInvoke(equipment.Helmet);
            }

            equipment.Helmet = equip;
            equipList.Remove(equip);
            equipment.Calculator.CalculateHelmet();
            OnEquipWearing?.Invoke(equipment.Helmet);

            //Save Data
            playerData.Helmet = equipment.Helmet;
            playerData.HelmetList = equipList;
        }
        
        if (equip.EquipSO.EquipType == EquipType.Armor)
        {
            if (equipment.Armor != null && equipment.Armor.Level > 0)
            {
                equipList.Add(equipment.Armor);
                equipment.Calculator.NewArmorBonus();
                equipment.Unequip.DisarmInvoke(equipment.Armor);
            }

            equipment.Armor = equip;
            equipList.Remove(equip);
            equipment.Calculator.CalculateArmor();
            OnEquipWearing?.Invoke(equipment.Armor);

            //Save Data
            playerData.Armor = equipment.Armor;
            playerData.ArmorList = equipList;
        }
        
        if (equip.EquipSO.EquipType == EquipType.Armwear)
        {
            if (equipment.Armwear != null && equipment.Armwear.Level > 0)
            {
                equipList.Add(equipment.Armwear);
                equipment.Calculator.NewArmwearBonus();
                equipment.Unequip.DisarmInvoke(equipment.Armwear);
            }

            equipment.Armwear = equip;
            equipList.Remove(equip);
            equipment.Calculator.CalculateArmwear();
            OnEquipWearing?.Invoke(equipment.Armwear);

            //Save Data
            playerData.Armwear = equipment.Armwear;
            playerData.ArmwearList = equipList;
        }
        
        if (equip.EquipSO.EquipType == EquipType.Boots)
        {
            if (equipment.Boots != null && equipment.Boots.Level > 0)
            {
                equipList.Add(equipment.Boots);
                equipment.Calculator.NewBootsBonus();
                equipment.Unequip.DisarmInvoke(equipment.Boots);
            }

            equipment.Boots = equip;
            equipList.Remove(equip);
            equipment.Calculator.CalculateBoots();
            OnEquipWearing?.Invoke(equipment.Boots);

            //Save Data
            playerData.Boots = equipment.Boots;
            playerData.BootsList = equipList;
        }
        
        if (equip.EquipSO.EquipType == EquipType.Special)
        {
            if (equipment.Special != null && equipment.Special.Level > 0)
            {
                equipList.Add(equipment.Special);
                equipment.Calculator.NewSpecialBonus();
                equipment.Unequip.DisarmInvoke(equipment.Special);
            }

            equipment.Special = equip;
            equipList.Remove(equip);
            equipment.Calculator.CalculateSpecial();
            OnEquipWearing?.Invoke(equipment.Special);

            //Save Data
            playerData.Special = equipment.Special;
            playerData.SpecialList = equipList;
        }

        equipment.Calculator.CalculateTotalBonus();
    }
}