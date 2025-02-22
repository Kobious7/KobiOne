using System.Collections.Generic;
using UnityEngine;

namespace InfiniteMap
{
    public class EquipWearing : InventoryAb
    {
        public void Wear(InventoryEquip equip)
        {
            List<InventoryEquip> equipList = Inventory.GetEquipListByEquipType(equip.EquipSO.EquipType);

            if(equip.EquipSO.EquipType == EquipType.WEAP)
            {
                Game.Instance.Equipment.Weapon = equip;
                equipList.Remove(equip);
                Game.Instance.Equipment.Calculator.CalculateWeapon();
            }
            if(equip.EquipSO.EquipType == EquipType.HELMET)
            {
                Game.Instance.Equipment.Helmet = equip;
                equipList.Remove(equip);
                Game.Instance.Equipment.Calculator.CalculateHelmet();
            }

            Game.Instance.Player.Stats.UpdateBonus(Game.Instance.Equipment.StatsBonus);
        }
    }
}