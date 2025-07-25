using System.Collections.Generic;
using UnityEngine;

public class EquipObtainer : InventoryAb
{
    [SerializeField] private List<EquipStatType> weaponMainStats;
    [SerializeField] private List<TypeBonus> typeBonus;
    [SerializeField] private List<EquipStatType> randomSubStatsFor5;
    [SerializeField] private List<EquipStatType> randomSubStatsFor1;
    [SerializeField] private List<EquipStatBonus> rarityBonusDefault;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadAllList();
    }

    private void LoadAllList()
    {
        weaponMainStats = new List<EquipStatType> { EquipStatType.Attack, EquipStatType.MagicAttack };
        typeBonus = new List<TypeBonus> { TypeBonus.FlatBonus, TypeBonus.PercentBonus };
        randomSubStatsFor5 = new List<EquipStatType>{ EquipStatType.Power, EquipStatType.Magic, EquipStatType.Strength,
                                                            EquipStatType.DefenseP, EquipStatType.Dexterity, EquipStatType.CritRate,
                                                            EquipStatType.CritDamage };
        randomSubStatsFor1 = new List<EquipStatType>{ EquipStatType.Power, EquipStatType.Magic, EquipStatType.Strength,
                                                            EquipStatType.DefenseP, EquipStatType.Dexterity, EquipStatType.Attack,
                                                            EquipStatType.MagicAttack, EquipStatType.HP, EquipStatType.Defense,
                                                            EquipStatType.Accuracy, EquipStatType.DamageRange, EquipStatType.CritRate,
                                                            EquipStatType.CritDamage };
    }

    public InventoryEquip CreateEquip(EquipSO equipSO, Rarity rarity)
    {
        Debug.Log(rarity);
        InventoryEquip newEquip = new InventoryEquip();
        newEquip.Level = 1;
        newEquip.EquipSO = equipSO;
        newEquip.Rarity = rarity;
        newEquip.MainStat = CreateMainStat(equipSO, rarity);
        newEquip.SubStats = new List<EquipStat>();
        newEquip.CurrentUpgradeCost = CalculateCostByRarity(newEquip.Level, newEquip.Rarity);
        newEquip.NextUpgradeCost = CalculateCostByRarity(newEquip.Level + 1, newEquip.Rarity);
        newEquip.IsNew = true;
        newEquip.IsLock = false;
        return newEquip;
    }

    private EquipStat CreateMainStat(EquipSO equipSO, Rarity rarity)
    {
        EquipType equipType = equipSO.EquipType;
        EquipStat mainStat = new EquipStat();

        if (equipType == EquipType.Weapon) CreateWeaponMainStat(mainStat, rarity);
        if (equipType == EquipType.Helmet) CreateHelmetMainStat(mainStat, rarity);
        if (equipType == EquipType.Armor) CreateArmorMainStat(mainStat, rarity);
        if (equipType == EquipType.Armwear) CreateArmwearMainStat(mainStat, rarity);
        if (equipType == EquipType.Boots) CreateBootsMainStat(mainStat, rarity);

        return mainStat;
    }

    public void CheckPrimarionSoulAndUpgrading(InventoryEquip equip)
    {
        if (Inventory.PrimarionSoul >= equip.CurrentUpgradeCost)
        {
            Inventory.PrimarionSoul -= equip.CurrentUpgradeCost;

            if (Inventory.PrimarionSoul < 0) Inventory.PrimarionSoul = 0;

            UpgradeEquip(equip, 1);

            //Sava Data
            InfiniteMapManager.Instance.PlayerData.PrimarionSoul = Inventory.PrimarionSoul;
        }
    }

    public void UpgradeEquip(InventoryEquip equip, int level)
    {
        equip.Level += level;
        equip.CurrentUpgradeCost = CalculateCostByRarity(equip.Level, equip.Rarity);
        equip.NextUpgradeCost = CalculateCostByRarity(equip.Level + 1, equip.Rarity);

        if (equip.Level >= 10 && equip.SubStats.Count <= 0)
        {
            equip.SubStats.Add(CreateSubStat(equip));
        }
        if (equip.Level >= 20 && equip.SubStats.Count == 1)
        {
            equip.SubStats.Add(CreateSubStat(equip));
        }
        if (equip.Level >= 30 && equip.SubStats.Count == 2)
        {
            equip.SubStats.Add(CreateSubStat(equip));
        }
        if (equip.Level >= 40 && equip.SubStats.Count == 3)
        {
            equip.SubStats.Add(CreateSubStat(equip));
        }
        if (equip.Level >= 50 && equip.SubStats.Count == 4)
        {
            equip.SubStats.Add(CreateSubStat(equip));
        }

        UpgradeAllStats(equip);
    }

    private EquipStat CreateSubStat(InventoryEquip equip)
    {
        EquipStat subStat = new EquipStat();
        EquipStatBonus defaultBonus = GetRarityBonus(equip.Rarity);

        if (equip.SubStats.Count <= 0)
        {
            if (equip.EquipSO.EquipType == EquipType.Weapon)
            {
                int flatValue = Random.Range((int)defaultBonus.SubStat.FlatMinValue, (int)defaultBonus.SubStat.FlatMaxValue + 1);
                subStat.Stat = EquipStatType.Accuracy;
                subStat.TypeBonus = TypeBonus.FlatBonus;
                subStat.FlatValue = flatValue;
                subStat.FlatBaseValue = flatValue;
                subStat.FlatBonus = defaultBonus.SubStat.FlatBonus;

                return subStat;
            }
            if (equip.EquipSO.EquipType == EquipType.Boots)
            {
                subStat.Stat = EquipStatType.Speed;
                subStat.TypeBonus = TypeBonus.FlatBonus;
                subStat.FlatValue = 1;

                return subStat;
            }
        }

        EquipStatType statType = EquipStatType.Power;

        if (equip.EquipSO.EquipType == EquipType.Weapon || equip.EquipSO.EquipType == EquipType.Helmet
            || equip.EquipSO.EquipType == EquipType.Armor || equip.EquipSO.EquipType == EquipType.Armwear || equip.EquipSO.EquipType == EquipType.Boots)
        {
            statType = randomSubStatsFor5[Random.Range(0, randomSubStatsFor5.Count)];

            if (CheckStatUnique(equip.SubStats, EquipStatType.CritRate))
            {
                while (statType == EquipStatType.CritRate) statType = randomSubStatsFor5[Random.Range(0, randomSubStatsFor5.Count)];
            }
            if (CheckStatUnique(equip.SubStats, EquipStatType.CritDamage))
            {
                while (statType == EquipStatType.CritDamage) statType = randomSubStatsFor5[Random.Range(0, randomSubStatsFor5.Count)];
            }
        }

        if (equip.EquipSO.EquipType == EquipType.Special)
        {
            statType = randomSubStatsFor1[Random.Range(0, randomSubStatsFor1.Count)];

            if (CheckStatUnique(equip.SubStats, EquipStatType.CritRate))
            {
                while (statType == EquipStatType.CritRate) statType = randomSubStatsFor1[Random.Range(0, randomSubStatsFor1.Count)];
            }
            if (CheckStatUnique(equip.SubStats, EquipStatType.CritDamage))
            {
                while (statType == EquipStatType.CritDamage) statType = randomSubStatsFor1[Random.Range(0, randomSubStatsFor1.Count)];
            }
        }

        TypeBonus bonusT = typeBonus[Random.Range(0, typeBonus.Count)];

        if (statType == EquipStatType.DamageRange || statType == EquipStatType.CritRate || statType == EquipStatType.CritDamage)
        {
            bonusT = TypeBonus.PercentBonus;
        }

        subStat.Stat = statType;

        if (bonusT == TypeBonus.FlatBonus)
        {
            int flatValue = Random.Range((int)defaultBonus.SubStat.FlatMinValue, (int)defaultBonus.SubStat.FlatMaxValue + 1);
            subStat.TypeBonus = TypeBonus.FlatBonus;
            subStat.FlatValue = flatValue;
            subStat.FlatBaseValue = flatValue;
            subStat.FlatBonus = defaultBonus.SubStat.FlatBonus;
        }

        if (bonusT == TypeBonus.PercentBonus)
        {
            float percentValue = Random.Range(defaultBonus.SubStat.PercentMinValue, defaultBonus.SubStat.PercentMaxValue);
            subStat.TypeBonus = TypeBonus.PercentBonus;
            subStat.PercentValue = percentValue;
            subStat.PercentBaseValue = percentValue;
            subStat.PercentBonus = defaultBonus.SubStat.PercentBonus;
        }

        return subStat;
    }

    private void UpgradeAllStats(InventoryEquip equip)
    {
        UpgradeMainStat(equip);
        if (equip.SubStats.Count <= 0) return;
        UpgradeSubStats(equip);
    }

    private void UpgradeMainStat(InventoryEquip equip)
    {
        if (equip.MainStat.TypeBonus == TypeBonus.FlatBonus)
        {
            equip.MainStat.FlatValue = equip.MainStat.FlatBaseValue + (equip.Level - 1) * equip.MainStat.FlatBonus;
        }
        else
        {
            equip.MainStat.PercentValue = equip.MainStat.PercentBaseValue + (equip.Level - 1) * equip.MainStat.PercentBonus;
        }
    }

    private void UpgradeSubStats(InventoryEquip equip)
    {
        int index = 0;
        foreach (var subStat in equip.SubStats)
        {
            if (subStat.TypeBonus == TypeBonus.FlatBonus)
            {
                subStat.FlatValue = subStat.FlatBaseValue + (equip.Level - 1) * subStat.FlatBonus;
            }
            else
            {
                subStat.PercentValue = subStat.PercentBaseValue + (equip.Level - 1) * subStat.PercentBonus;

                if (subStat.Stat == EquipStatType.CritRate) subStat.PercentValue = subStat.PercentValue > 14.3f ? 14.3f : subStat.PercentValue;
                if (subStat.Stat == EquipStatType.CritDamage) subStat.PercentValue = subStat.PercentValue > 100 ? 100 : subStat.PercentValue;
                if (subStat.Stat == EquipStatType.DamageRange) subStat.PercentValue = subStat.PercentValue > 28.6f ? 28.6f : subStat.PercentValue;
            }

            if (index == 0)
            {
                if (equip.EquipSO.EquipType == EquipType.Boots)
                {
                    int speedBonus = equip.Level / 10 + 1;
                    speedBonus = speedBonus > 6 ? 6 : speedBonus;
                    subStat.FlatValue = speedBonus;
                }
            }

            index++;
        }
    }

    private bool CheckStatUnique(List<EquipStat> subStats, EquipStatType statType)
    {
        foreach (var stat in subStats)
        {
            if (stat.Stat == statType) return true;
        }

        return false;
    }

    private void CreateWeaponMainStat(EquipStat mainStat, Rarity rarity)
    {
        EquipStatType equipStatType = weaponMainStats[Random.Range(0, weaponMainStats.Count)];
        EquipStatBonus defaultBonus = GetRarityBonus(rarity);
        int flatValue = Random.Range((int)defaultBonus.MainStat.FlatMinValue, (int)defaultBonus.MainStat.FlatMaxValue + 1);
        mainStat.Stat = equipStatType;
        mainStat.TypeBonus = TypeBonus.FlatBonus;
        mainStat.FlatValue = flatValue;
        mainStat.FlatBaseValue = flatValue;
        mainStat.FlatBonus = defaultBonus.MainStat.FlatBonus;
    }

    private void CreateHelmetMainStat(EquipStat mainStat, Rarity rarity)
    {
        EquipStatBonus defaultBonus = GetRarityBonus(rarity);
        int flatValue = Random.Range((int)defaultBonus.MainStat.FlatMinValue, (int)defaultBonus.MainStat.FlatMaxValue + 1);
        mainStat.Stat = EquipStatType.Defense;
        mainStat.TypeBonus = TypeBonus.FlatBonus;
        mainStat.FlatValue = flatValue;
        mainStat.FlatBaseValue = flatValue;
        mainStat.FlatBonus = defaultBonus.MainStat.FlatBonus;
    }

    private void CreateArmorMainStat(EquipStat mainStat, Rarity rarity)
    {
        EquipStatBonus defaultBonus = GetRarityBonus(rarity);
        float percentValue = Random.Range(defaultBonus.MainStat.PercentMinValue, defaultBonus.MainStat.PercentMaxValue);
        mainStat.Stat = EquipStatType.Defense;
        mainStat.TypeBonus = TypeBonus.PercentBonus;
        mainStat.PercentValue = percentValue;
        mainStat.PercentBaseValue = percentValue;
        mainStat.PercentBonus = defaultBonus.MainStat.PercentBonus;
    }

    private void CreateArmwearMainStat(EquipStat mainStat, Rarity rarity)
    {
        EquipStatBonus defaultBonus = GetRarityBonus(rarity);
        int flatValue = Random.Range((int)defaultBonus.MainStat.FlatMinValue, (int)defaultBonus.MainStat.FlatMaxValue + 1);
        mainStat.Stat = EquipStatType.HP;
        mainStat.TypeBonus = TypeBonus.FlatBonus;
        mainStat.FlatValue = flatValue;
        mainStat.FlatBaseValue = flatValue;
        mainStat.FlatBonus = defaultBonus.MainStat.FlatBonus;
    }

    private void CreateBootsMainStat(EquipStat mainStat, Rarity rarity)
    {
        EquipStatBonus defaultBonus = GetRarityBonus(rarity);
        float percentValue = Random.Range(defaultBonus.MainStat.PercentMinValue, defaultBonus.MainStat.PercentMaxValue);
        mainStat.Stat = EquipStatType.HP;
        mainStat.TypeBonus = TypeBonus.PercentBonus;
        mainStat.PercentValue = percentValue;
        mainStat.PercentBaseValue = percentValue;
        mainStat.PercentBonus = defaultBonus.MainStat.PercentBonus;
    }

    private EquipStatBonus GetRarityBonus(Rarity rarity)
    {
        foreach (var rarityBonus in rarityBonusDefault)
        {
            if (rarity == rarityBonus.Rarity) return rarityBonus;
        }

        return null;
    }

    private Rarity GetRarity()
    {
        float rate = 1; //Random.Range(0f, 100f);

        if (rate <= (int)Rarity.Lengendary) return Rarity.Lengendary;
        if (rate <= (int)Rarity.Epic) return Rarity.Epic;
        if (rate <= (int)Rarity.Rare) return Rarity.Rare;
        if (rate <= (int)Rarity.Uncommon) return Rarity.Uncommon;
        if (rate <= (int)Rarity.Common) return Rarity.Common;
        return Rarity.None;
    }

    public EquipStatBonus GetDefalutRarityBounusByRarity(Rarity rarity)
    {
        foreach (EquipStatBonus defalutBonus in rarityBonusDefault)
        {
            if (defalutBonus.Rarity == rarity) return defalutBonus;
        }

        return null;
    }

    public int CalculateCostByRarity(int level,  Rarity rarity)
    {
        switch (rarity)
        {
            case Rarity.Common:
                return level * (int)(1f + 0.05f * level);
            case Rarity.Uncommon:
                return level * (int)(1.5f + 0.09f * level);
            case Rarity.Rare:
                return level * (int)(2f + 0.13f * level);
            case Rarity.Epic:
                return level * (int)(2.5f + 0.17f * level);
            case Rarity.Lengendary:
                return level * (int)(3f + 0.21f * level);
            case Rarity.Mythic:
                return level * (int)(3.5f + 0.25f * level);
            case Rarity.Divine:
                return level * (int)(4f + 0.29f * level);
            default:
                return 0;
        }
    }
}