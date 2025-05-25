using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EquipmentCalculation : EquipmentAb
{
    [SerializeField] private List<EquipBonus> weaponBonus;
    [SerializeField] private List<EquipBonus> helmetBonus;
    [SerializeField] private List<EquipBonus> bodyArmorBonus;
    [SerializeField] private List<EquipBonus> legArmorBonus;
    [SerializeField] private List<EquipBonus> bootsBonus;
    [SerializeField] private List<EquipBonus> backItemBonus;
    [SerializeField] private List<EquipBonus> auraBonus;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        NewWeaponBonus();
        NewHelmetBonus();
        NewBodyArmorBonus();
        NewLegArmorBonus();
        NewBootsBonus();
        NewBackItemBonus();
        NewAuraBonus();
    }

    public void CalculateBonus()
    {
        CalculatePerEquip(Equipment.Weapon);
        CalculatePerEquip(Equipment.Helmet);
        CalculatePerEquip(Equipment.BodyArmor);
        CalculatePerEquip(Equipment.LegArmor);
        CalculatePerEquip(Equipment.Boots);
        CalculatePerEquip(Equipment.BackItem);
        CalculatePerEquip(Equipment.Aura);
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
            int bodyArmorFlatBonus = Equipment.BodyArmor.Level <= 0 ? 0 : bodyArmorBonus[index].FlatValue;
            float bodyArmorPercentBonus = Equipment.BodyArmor.Level <= 0 ? 0 : bodyArmorBonus[index].PercentValue;
            int legArmorFlatBonus = Equipment.LegArmor.Level <= 0 ? 0 : legArmorBonus[index].FlatValue;
            float legArmorPercentBonus = Equipment.LegArmor.Level <= 0 ? 0 : legArmorBonus[index].PercentValue;
            int bootsFlatBonus = Equipment.Boots.Level <= 0 ? 0 : bootsBonus[index].FlatValue;
            float bootsPercentBonus = Equipment.Boots.Level <= 0 ? 0 : bootsBonus[index].PercentValue;
            int backItemFlatBonus = Equipment.BackItem.Level <= 0 ? 0 : backItemBonus[index].FlatValue;
            float backItemPercentBonus = Equipment.BackItem.Level <= 0 ? 0 : backItemBonus[index].PercentValue;
            int auraFlatBonus = Equipment.Aura.Level <= 0 ? 0 : auraBonus[index].FlatValue;
            float auraPercentBonus = Equipment.Aura.Level <= 0 ? 0 : auraBonus[index].PercentValue;

            bonus.FlatValue = weaponFlatBonus + helmetFlatBonus + bodyArmorFlatBonus + legArmorFlatBonus + bootsFlatBonus + backItemFlatBonus + auraFlatBonus;
            bonus.PercentValue = weaponPercentBonus + helmetPercentBonus + bodyArmorPercentBonus + legArmorPercentBonus + bootsPercentBonus + backItemPercentBonus + auraPercentBonus;
            index++;
        }
    }

    private List<EquipBonus> GetEquipBonusType(InventoryEquip equip)
    {
        if(equip.EquipSO.EquipType == EquipType.WEAP) return weaponBonus;
        else if(equip.EquipSO.EquipType == EquipType.HELMET) return helmetBonus;
        else if(equip.EquipSO.EquipType == EquipType.BODYARMOR) return bodyArmorBonus;
        else if(equip.EquipSO.EquipType == EquipType.LEGARMOR) return legArmorBonus;
        else if(equip.EquipSO.EquipType == EquipType.BOOTS) return bootsBonus;
        else if(equip.EquipSO.EquipType == EquipType.BACKITEM) return backItemBonus;
        else return auraBonus;
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

    public void NewBodyArmorBonus()
    {
        bodyArmorBonus = NewEquipBonus();
    }

    public void NewLegArmorBonus()
    {
        legArmorBonus = NewEquipBonus();
    }

    public void NewBootsBonus()
    {
        bootsBonus = NewEquipBonus();
    }

    public void NewBackItemBonus()
    {
        backItemBonus = NewEquipBonus();
    }

    public void NewAuraBonus()
    {
        auraBonus = NewEquipBonus();
    }
}