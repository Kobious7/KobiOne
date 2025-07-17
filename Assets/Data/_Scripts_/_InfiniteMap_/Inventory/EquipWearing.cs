using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipWearing : InventoryAb
{
    public event Action<InventoryEquip> OnEquipWearing;
    private Equipment equipment;

    protected override void Start()
    {
        base.Start();
        equipment = InfiniteMapManager.Instance.Equipment;
    }

    public void Equip(InventoryEquip equip)
    {
        Debug.Log(equip.EquipSO.EquipType);
        List<InventoryEquip> equipList = Inventory.GetEquipListByEquipType(equip.EquipSO.EquipType);

        if(equip.EquipSO.EquipType == EquipType.Weapon)
        {
            if(equipment.Weapon != null && equipment.Weapon.Level > 0) 
            {
                equipList.Add(equipment.Weapon);
                equipment.Calculator.NewWeaponBonus();
                equipment.Unequip.DisarmInvoke(equipment.Weapon);
            }

            equipment.Weapon = equip;
            equipList.Remove(equip);
            equipment.Calculator.CalculateWeapon();
            OnEquipWearing?.Invoke(equipment.Weapon);
        }
        if(equip.EquipSO.EquipType == EquipType.Helmet)
        {
            equipment.Helmet = equip;
            equipList.Remove(equip);
            equipment.Calculator.CalculateHelmet();
            OnEquipWearing?.Invoke(equipment.Helmet);
        }
        if(equip.EquipSO.EquipType == EquipType.Armor)
        {
            equipment.Armor = equip;
            equipList.Remove(equip);
            equipment.Calculator.CalculateArmor();
            OnEquipWearing?.Invoke(equipment.Armor);
        }
        if(equip.EquipSO.EquipType == EquipType.Armwear)
        {
            equipment.Armwear = equip;
            equipList.Remove(equip);
            equipment.Calculator.CalculateArmwear();
            OnEquipWearing?.Invoke(equipment.Armwear);
        }
        if(equip.EquipSO.EquipType == EquipType.Boots)
        {
            equipment.Boots = equip;
            equipList.Remove(equip);
            equipment.Calculator.CalculateBoots();
            OnEquipWearing?.Invoke(equipment.Boots);
        }
        if(equip.EquipSO.EquipType == EquipType.Special)
        {
            equipment.Special = equip;
            equipList.Remove(equip);
            equipment.Calculator.CalculateSpecial();
            OnEquipWearing?.Invoke(equipment.Special);
        }

        equipment.Calculator.CalculateTotalBonus();
    }
}