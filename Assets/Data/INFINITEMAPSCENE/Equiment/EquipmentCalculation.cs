using System.Collections.Generic;
using UnityEngine;

namespace InfiniteMap
{
    public class EquipmentCalculation : EquipmentAb
    {
        public void CalculateBonus()
        {
            CalculatePerEquip(Equipment.Weapon);
            CalculatePerEquip(Equipment.Helmet);
            CalculatePerEquip(Equipment.BodyArmor);
            CalculatePerEquip(Equipment.LegArmor);
            CalculatePerEquip(Equipment.Boots);
            CalculatePerEquip(Equipment.BackItem);
            CalculatePerEquip(Equipment.Aura);
        }

        public void CalculateWeapon()
        {
            CalculatePerEquip(Equipment.Weapon);
        }

        public void CalculateHelmet()
        {
            CalculatePerEquip(Equipment.Helmet);
        }

        public void CalculateBodyArmor()
        {
            CalculatePerEquip(Equipment.BodyArmor);
        }

        public void CalculateLegArmor()
        {
            CalculatePerEquip(Equipment.LegArmor);
        }

        public void CalculateBoots()
        {
            CalculatePerEquip(Equipment.Boots);
        }

        public void CalculateBackItem()
        {
            CalculatePerEquip(Equipment.BackItem);
        }

        public void CalculateAura()
        {
            CalculatePerEquip(Equipment.Aura);
        }

        private void CalculatePerEquip(InventoryEquip equip)
        {
            if(equip.Level <= 0) return;

            EquipBonus mainStatBonus = Equipment.GetEquipBonusByStat(equip.MainStat.Stat);

            if(equip.MainStat.TypeBonus == TypeBonus.FlatBonus)
            {
                mainStatBonus.FlatValue += equip.MainStat.FlatValue;
            }
            if(equip.MainStat.TypeBonus == TypeBonus.PercentBonus)
            {
                mainStatBonus.PercentValue += equip.MainStat.PercentValue;
            }

            foreach(var subStat in equip.SubStats)
            {
                EquipBonus subStatBonus = Equipment.GetEquipBonusByStat(subStat.Stat);

                if(subStat.TypeBonus == TypeBonus.FlatBonus)
                {
                    subStatBonus.FlatValue += subStat.FlatValue;
                }
                if(subStat.TypeBonus == TypeBonus.PercentBonus)
                {
                    subStatBonus.PercentValue += subStat.PercentValue;
                }
            }
        }
    }
}