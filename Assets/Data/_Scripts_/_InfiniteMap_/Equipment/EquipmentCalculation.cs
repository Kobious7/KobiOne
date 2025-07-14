using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EquipmentCalculation : EquipmentAb
{
    [SerializeField] private List<EquipBonus> weaponBonus;
    [SerializeField] private List<EquipBonus> helmetBonus;
    [SerializeField] private List<EquipBonus> armorBonus;
    [SerializeField] private List<EquipBonus> armwearBonus;
    [SerializeField] private List<EquipBonus> bootsBonus;
    [SerializeField] private List<EquipBonus> specialBonus;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        NewWeaponBonus();
        NewHelmetBonus();
        NewArmorBonus();
        NewArmwearBonus();
        NewBootsBonus();
        NewSpecialBonus();
    }

    public void CalculateBonus()
    {
        CalculatePerEquip(Equipment.Weapon);
        CalculatePerEquip(Equipment.Helmet);
        CalculatePerEquip(Equipment.Armor);
        CalculatePerEquip(Equipment.Armwear);
        CalculatePerEquip(Equipment.Boots);
        CalculatePerEquip(Equipment.Special);
        CalculateTotalBonus();
    }

    public void CalculateWeapon()
    {
        CalculatePerEquip(Equipment.Weapon);
    }

    public void CalculateHelmet()
    {
        CalculatePerEquip(Equipment.Helmet);
    }

    public void CalculateArmor()
    {
        CalculatePerEquip(Equipment.Armor);
    }

    public void CalculateArmwear()
    {
        CalculatePerEquip(Equipment.Armwear);
    }

    public void CalculateBoots()
    {
        CalculatePerEquip(Equipment.Boots);
    }

    public void CalculateSpecial()
    {
        CalculatePerEquip(Equipment.Special);
    }

    private void CalculatePerEquip(InventoryEquip equip)
    {
        if(equip.Level <= 0) return;

        List<EquipBonus> equipBonus = GetEquipBonusType(equip);
        EquipBonus mainStatBonus = GetEquipBonusByStat(equipBonus, equip.MainStat.Stat);

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
            EquipBonus subStatBonus = GetEquipBonusByStat(equipBonus, subStat.Stat);

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

    public void CalculateTotalBonus()
    {
        int index = 0;

        Equipment.NewStatBonus();

        foreach(var bonus in Equipment.StatsBonus)
        {
            int weaponFlatBonus = Equipment.Weapon.Level <= 0 ? 0 : weaponBonus[index].FlatValue;
            float weaponPercentBonus = Equipment.Weapon.Level <= 0 ? 0 : weaponBonus[index].PercentValue;
            int helmetFlatBonus = Equipment.Helmet.Level <= 0 ? 0 : helmetBonus[index].FlatValue;
            float helmetPercentBonus = Equipment.Helmet.Level <= 0 ? 0 : helmetBonus[index].PercentValue;
            int armorFlatBonus = Equipment.Armor.Level <= 0 ? 0 : armorBonus[index].FlatValue;
            float armorPercentBonus = Equipment.Armor.Level <= 0 ? 0 : armorBonus[index].PercentValue;
            int armwearFlatBonus = Equipment.Armwear.Level <= 0 ? 0 : armwearBonus[index].FlatValue;
            float armwearPercentBonus = Equipment.Armwear.Level <= 0 ? 0 : armwearBonus[index].PercentValue;
            int bootsFlatBonus = Equipment.Boots.Level <= 0 ? 0 : bootsBonus[index].FlatValue;
            float bootsPercentBonus = Equipment.Boots.Level <= 0 ? 0 : bootsBonus[index].PercentValue;
            int specialFlatBonus = Equipment.Special.Level <= 0 ? 0 : specialBonus[index].FlatValue;
            float specialPercentBonus = Equipment.Special.Level <= 0 ? 0 : specialBonus[index].PercentValue;

            bonus.FlatValue = weaponFlatBonus + helmetFlatBonus + armorFlatBonus + armwearFlatBonus + bootsFlatBonus + specialFlatBonus;
            bonus.PercentValue = weaponPercentBonus + helmetPercentBonus + armorPercentBonus + armwearPercentBonus + bootsPercentBonus + specialPercentBonus;
            index++;
        }
    }

    private List<EquipBonus> GetEquipBonusType(InventoryEquip equip)
    {
        if(equip.EquipSO.EquipType == EquipType.Weapon) return weaponBonus;
        else if(equip.EquipSO.EquipType == EquipType.Helmet) return helmetBonus;
        else if(equip.EquipSO.EquipType == EquipType.Armor) return armorBonus;
        else if(equip.EquipSO.EquipType == EquipType.Armwear) return armwearBonus;
        else if(equip.EquipSO.EquipType == EquipType.Boots) return bootsBonus;
        else return specialBonus;
    }

    public EquipBonus GetEquipBonusByStat(List<EquipBonus> equipBonus, EquipStatType statType)
    {
        foreach(var stat in equipBonus)
        {
            if(stat.Stat == statType) return stat;
        }

        return null;
    }

    private List<EquipBonus> NewEquipBonus()
    {
        return new List<EquipBonus>
        {
            new EquipBonus(EquipStatType.Power), new EquipBonus(EquipStatType.Magic), new EquipBonus(EquipStatType.Strength),
            new EquipBonus(EquipStatType.DefenseP), new EquipBonus(EquipStatType.Dexterity), new EquipBonus(EquipStatType.Attack),
            new EquipBonus(EquipStatType.MagicAttack), new EquipBonus(EquipStatType.HP), new EquipBonus(EquipStatType.Defense),
            new EquipBonus(EquipStatType.Accuracy), new EquipBonus(EquipStatType.DamageRange), new EquipBonus(EquipStatType.Speed),
            new EquipBonus(EquipStatType.CritRate), new EquipBonus(EquipStatType.CritDamage)
        };
    }

    public void NewWeaponBonus()
    {
        weaponBonus = NewEquipBonus();
    }

    public void NewHelmetBonus()
    {
        helmetBonus = NewEquipBonus();
    }

    public void NewArmorBonus()
    {
        armorBonus = NewEquipBonus();
    }

    public void NewArmwearBonus()
    {
        armwearBonus = NewEquipBonus();
    }

    public void NewBootsBonus()
    {
        bootsBonus = NewEquipBonus();
    }

    public void NewSpecialBonus()
    {
        specialBonus = NewEquipBonus();
    }
}