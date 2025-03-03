using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InfiniteMap
{
    public class EquipWearing : InventoryAb
    {
        public event Action<InventoryEquip> OnEquipWearing;
        private Equipment equipment;

        protected override void Start()
        {
            base.Start();
            equipment = Game.Instance.Equipment;
        }

        public void Equip(InventoryEquip equip)
        {
            Debug.Log(equip.EquipSO.EquipType);
            List<InventoryEquip> equipList = Inventory.GetEquipListByEquipType(equip.EquipSO.EquipType);

            if(equip.EquipSO.EquipType == EquipType.WEAP)
            {
                equipment.Weapon = equip;
                equipList.Remove(equip);
                equipment.Calculator.CalculateWeapon();
                Game.Instance.Player.SpriteSwap.SetSpriteResolver(1, equip.EquipSO.ItemName);
                OnEquipWearing?.Invoke(equipment.Weapon);
            }
            if(equip.EquipSO.EquipType == EquipType.HELMET)
            {
                equipment.Helmet = equip;
                equipList.Remove(equip);
                equipment.Calculator.CalculateHelmet();
                Game.Instance.Player.SpriteSwap.SetSpriteResolver(0, equip.EquipSO.ItemName);
                OnEquipWearing?.Invoke(equipment.Helmet);
            }
            if(equip.EquipSO.EquipType == EquipType.BODYARMOR)
            {
                equipment.BodyArmor = equip;
                equipList.Remove(equip);
                equipment.Calculator.CalculateBodyArmor();
                OnEquipWearing?.Invoke(equipment.BodyArmor);
            }
            if(equip.EquipSO.EquipType == EquipType.LEGARMOR)
            {
                equipment.LegArmor = equip;
                equipList.Remove(equip);
                equipment.Calculator.CalculateLegArmor();
                OnEquipWearing?.Invoke(equipment.LegArmor);
            }
            if(equip.EquipSO.EquipType == EquipType.BOOTS)
            {
                equipment.Boots = equip;
                equipList.Remove(equip);
                equipment.Calculator.CalculateBoots();
                OnEquipWearing?.Invoke(equipment.Boots);
            }
            if(equip.EquipSO.EquipType == EquipType.BACKITEM)
            {
                equipment.BackItem = equip;
                equipList.Remove(equip);
                equipment.Calculator.CalculateBackItem();
                OnEquipWearing?.Invoke(equipment.BackItem);
            }
            if(equip.EquipSO.EquipType == EquipType.AURA)
            {
                equipment.Aura = equip;
                equipList.Remove(equip);
                equipment.Calculator.CalculateAura();
                OnEquipWearing?.Invoke(equipment.Aura);
            }

            equipment.Calculator.CalculateTotalBonus();
            Game.Instance.Player.Stats.UpdateBonus(equipment.StatsBonus);
        }
    }
}