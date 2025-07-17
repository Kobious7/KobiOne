using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EquipmentCalculator : EquipmentAb
{
    public event Action<List<OtherSourcesBonus>> OnTotalBonusChanged;
    [SerializeField] private List<OtherSourcesBonus> weaponBonus;
    [SerializeField] private List<OtherSourcesBonus> helmetBonus;
    [SerializeField] private List<OtherSourcesBonus> armorBonus;
    [SerializeField] private List<OtherSourcesBonus> armwearBonus;
    [SerializeField] private List<OtherSourcesBonus> bootsBonus;
    [SerializeField] private List<OtherSourcesBonus> specialBonus;

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

    public void InitTotalBonus()
    {
        InfiniteMapSO mapData = InfiniteMapManager.Instance.MapData;

        if (mapData.Weapon != null) CalculatePerEquip(mapData.Weapon);
        if (mapData.Helmet != null) CalculatePerEquip(mapData.Helmet);
        if (mapData.Armor != null) CalculatePerEquip(mapData.Armor);
        if (mapData.Armwear != null) CalculatePerEquip(mapData.Armwear);
        if (mapData.Boots != null) CalculatePerEquip(mapData.Boots);
        if (mapData.Special != null) CalculatePerEquip(mapData.Special);
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

        List<OtherSourcesBonus> equipBonus = GetEquipBonusType(equip);
        OtherSourcesBonus mainStatBonus = GetEquipBonusByStat(equipBonus, equip.MainStat.Stat);

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
            OtherSourcesBonus subStatBonus = GetEquipBonusByStat(equipBonus, subStat.Stat);

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

        foreach (var bonus in Equipment.StatsBonus)
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

        OnTotalBonusChanged?.Invoke(Equipment.StatsBonus);
    }

    private List<OtherSourcesBonus> GetEquipBonusType(InventoryEquip equip)
    {
        if(equip.EquipSO.EquipType == EquipType.Weapon) return weaponBonus;
        else if(equip.EquipSO.EquipType == EquipType.Helmet) return helmetBonus;
        else if(equip.EquipSO.EquipType == EquipType.Armor) return armorBonus;
        else if(equip.EquipSO.EquipType == EquipType.Armwear) return armwearBonus;
        else if(equip.EquipSO.EquipType == EquipType.Boots) return bootsBonus;
        else return specialBonus;
    }

    public OtherSourcesBonus GetEquipBonusByStat(List<OtherSourcesBonus> equipBonus, EquipStatType statType)
    {
        foreach(var stat in equipBonus)
        {
            if(stat.Stat == statType) return stat;
        }

        return null;
    }

    private List<OtherSourcesBonus> NewEquipBonus()
    {
        return new List<OtherSourcesBonus>
        {
            new OtherSourcesBonus(EquipStatType.Power), new OtherSourcesBonus(EquipStatType.Magic), new OtherSourcesBonus(EquipStatType.Strength),
            new OtherSourcesBonus(EquipStatType.DefenseP), new OtherSourcesBonus(EquipStatType.Dexterity), new OtherSourcesBonus(EquipStatType.Attack),
            new OtherSourcesBonus(EquipStatType.MagicAttack), new OtherSourcesBonus(EquipStatType.HP), new OtherSourcesBonus(EquipStatType.Defense),
            new OtherSourcesBonus(EquipStatType.Accuracy), new OtherSourcesBonus(EquipStatType.DamageRange), new OtherSourcesBonus(EquipStatType.Speed),
            new OtherSourcesBonus(EquipStatType.CritRate), new OtherSourcesBonus(EquipStatType.CritDamage)
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