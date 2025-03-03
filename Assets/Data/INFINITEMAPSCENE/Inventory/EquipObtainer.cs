using System.Collections.Generic;
using UnityEngine;

namespace InfiniteMap
{
    public class EquipObtainer : InventoryAb
    {
        [SerializeField] private List<EquipStatType> weaponMainStats;
        [SerializeField] private List<TypeBonus> typeBonus;
        [SerializeField] private List<EquipStatType> randomSubStatsFor5;
        [SerializeField] private List<EquipStatType> randomSubStatsFor2;
        [SerializeField] private List<EquipStatBonus> rarityBonusDefault;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadAllList();
        }

        private void LoadAllList()
        {
            weaponMainStats = new List<EquipStatType>{ EquipStatType.Attack, EquipStatType.MagicAttack };
            typeBonus = new List<TypeBonus>{ TypeBonus.FlatBonus, TypeBonus.PercentBonus };
            randomSubStatsFor5 = new List<EquipStatType>{ EquipStatType.Power, EquipStatType.Magic, EquipStatType.Strength,
                                                                EquipStatType.DefenseP, EquipStatType.Dexterity, EquipStatType.CritRate,
                                                                EquipStatType.CritDamage };
            randomSubStatsFor2 = new List<EquipStatType>{ EquipStatType.Power, EquipStatType.Magic, EquipStatType.Strength,
                                                                EquipStatType.DefenseP, EquipStatType.Dexterity, EquipStatType.Attack,
                                                                EquipStatType.MagicAttack, EquipStatType.HP, EquipStatType.Defense,
                                                                EquipStatType.Accuracy, EquipStatType.DamageRange, EquipStatType.CritRate,
                                                                EquipStatType.CritDamage };
        }

        public InventoryEquip CreateEquip(EquipSO equipSO)
        {
            Rarity rarity = GetRarity();
            Debug.Log(rarity);
            if(rarity == Rarity.None) return null;

            InventoryEquip newEquip = new InventoryEquip();
            newEquip.Level = 1;
            newEquip.EquipSO = equipSO;
            newEquip.Rarity = rarity;
            newEquip.MainStat = CreateMainStat(equipSO, rarity);
            newEquip.SubStats = new List<EquipStat>();
            return newEquip;
        }

        private EquipStat CreateMainStat(EquipSO equipSO, Rarity rarity)
        {
            EquipType equipType = equipSO.EquipType;
            EquipStat mainStat = new EquipStat();

            if(equipType == EquipType.WEAP) CreateWeaponMainStat(mainStat, rarity);
            if(equipType == EquipType.HELMET) CreateHelmetMainStat(mainStat, rarity);
            if(equipType == EquipType.BODYARMOR) CreateBodyArmorMainStat(mainStat, rarity);
            if(equipType == EquipType.LEGARMOR) CreateLegArmorMainStat(mainStat, rarity);
            if(equipType == EquipType.BOOTS) CreateBootsMainStat(mainStat, rarity);

            return mainStat;
        }

        public void UpgradeEquip(InventoryEquip equip, int level)
        {
            equip.Level += level;

            if(level >= 10 && equip.SubStats.Count <= 0)
            {
                equip.SubStats.Add(CreateSubStat(equip));
            }
            if(level >= 20 && equip.SubStats.Count == 1)
            {
                equip.SubStats.Add(CreateSubStat(equip));
            }
            if(level >= 30 && equip.SubStats.Count == 2)
            {
                equip.SubStats.Add(CreateSubStat(equip));
            }
            if(level >= 40 && equip.SubStats.Count == 3)
            {
                equip.SubStats.Add(CreateSubStat(equip));
            }
            if(level >= 50 && equip.SubStats.Count == 4)
            {
                equip.SubStats.Add(CreateSubStat(equip));
            }

            UpgradeAllStats(equip);
        }

        private EquipStat CreateSubStat(InventoryEquip equip)
        {
            EquipStat subStat = new EquipStat();
            EquipStatBonus defaultBonus = GetRarityBonus(equip.Rarity);

            if(equip.SubStats.Count <= 0)
            {
                if(equip.EquipSO.EquipType == EquipType.WEAP)
                {
                    int flatValue = Random.Range((int)defaultBonus.SubStat.FlatMinValue, (int)defaultBonus.SubStat.FlatMaxValue + 1);
                    subStat.Stat = EquipStatType.Accuracy;
                    subStat.TypeBonus = TypeBonus.FlatBonus;
                    subStat.FlatValue = flatValue;
                    subStat.FlatBaseValue = flatValue;
                    subStat.FlatBonus = defaultBonus.SubStat.FlatBonus;

                    return subStat;
                }
                if(equip.EquipSO.EquipType == EquipType.BOOTS)
                {
                    subStat.Stat = EquipStatType.Speed;
                    subStat.TypeBonus = TypeBonus.FlatBonus;
                    subStat.FlatValue = 1;

                    return subStat;
                }
            }

            EquipStatType statType = EquipStatType.Power;
            
            if(equip.EquipSO.EquipType == EquipType.WEAP || equip.EquipSO.EquipType == EquipType.HELMET
                || equip.EquipSO.EquipType == EquipType.BODYARMOR || equip.EquipSO.EquipType == EquipType.LEGARMOR || equip.EquipSO.EquipType == EquipType.BOOTS)
            {
                statType = randomSubStatsFor5[Random.Range(0, randomSubStatsFor5.Count)];

                if(CheckStatUnique(equip.SubStats, EquipStatType.CritRate))
                {
                    while(statType == EquipStatType.CritRate) statType = randomSubStatsFor5[Random.Range(0, randomSubStatsFor5.Count)];
                }
                if(CheckStatUnique(equip.SubStats, EquipStatType.CritDamage))
                {
                    while(statType == EquipStatType.CritDamage) statType = randomSubStatsFor5[Random.Range(0, randomSubStatsFor5.Count)];
                }
            }

            if(equip.EquipSO.EquipType == EquipType.BACKITEM || equip.EquipSO.EquipType == EquipType.AURA)
            {
                statType = randomSubStatsFor2[Random.Range(0, randomSubStatsFor2.Count)];

                if(CheckStatUnique(equip.SubStats, EquipStatType.CritRate))
                {
                    while(statType == EquipStatType.CritRate) statType = randomSubStatsFor2[Random.Range(0, randomSubStatsFor2.Count)];
                }
                if(CheckStatUnique(equip.SubStats, EquipStatType.CritDamage))
                {
                    while(statType == EquipStatType.CritDamage) statType = randomSubStatsFor2[Random.Range(0, randomSubStatsFor2.Count)];
                }
            }

            TypeBonus bonusT = typeBonus[Random.Range(0, typeBonus.Count)];

            if(statType == EquipStatType.DamageRange || statType == EquipStatType.CritRate || statType == EquipStatType.CritDamage)
            {
                bonusT = TypeBonus.PercentBonus;
            }

            subStat.Stat = statType;

            if(bonusT == TypeBonus.FlatBonus)
            {
                int flatValue = Random.Range((int)defaultBonus.SubStat.FlatMinValue, (int)defaultBonus.SubStat.FlatMaxValue + 1);
                subStat.TypeBonus = TypeBonus.FlatBonus;
                subStat.FlatValue = flatValue;
                subStat.FlatBaseValue = flatValue;
                subStat.FlatBonus = defaultBonus.SubStat.FlatBonus;
            }

            if(bonusT == TypeBonus.PercentBonus)
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
            if(equip.SubStats.Count <= 0) return;
            UpgradeSubStats(equip);
        }

        private void UpgradeMainStat(InventoryEquip equip)
        {  
            if(equip.MainStat.TypeBonus == TypeBonus.FlatBonus)
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
            foreach(var subStat in equip.SubStats)
            {
                if(subStat.TypeBonus == TypeBonus.FlatBonus)
                {
                    subStat.FlatValue = subStat.FlatBaseValue + (equip.Level - 1) * subStat.FlatBonus;
                }
                else
                {
                    subStat.PercentValue = subStat.PercentBaseValue + (equip.Level - 1) * subStat.PercentBonus;

                    if(subStat.Stat == EquipStatType.CritRate) subStat.PercentValue = subStat.PercentValue > 14.3f ? 14.3f : subStat.PercentValue;
                    if(subStat.Stat == EquipStatType.CritDamage) subStat.PercentValue = subStat.PercentValue > 100 ? 100 : subStat.PercentValue;
                    if(subStat.Stat == EquipStatType.DamageRange) subStat.PercentValue = subStat.PercentValue > 28.6f ? 28.6f : subStat.PercentValue;
                }

                if(index == 0)
                {
                    if(equip.EquipSO.EquipType == EquipType.BOOTS)
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
            foreach(var stat in subStats)
            {
                if(stat.Stat == statType) return true;
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
            mainStat.Stat = EquipStatType.HP;
            mainStat.TypeBonus = TypeBonus.FlatBonus;
            mainStat.FlatValue = flatValue;
            mainStat.FlatBaseValue = flatValue;
            mainStat.FlatBonus = defaultBonus.MainStat.FlatBonus;
        }

        private void CreateBodyArmorMainStat(EquipStat mainStat, Rarity rarity)
        {
            EquipStatBonus defaultBonus = GetRarityBonus(rarity);
            int flatValue = Random.Range((int)defaultBonus.MainStat.FlatMinValue, (int)defaultBonus.MainStat.FlatMaxValue + 1);
            mainStat.Stat = EquipStatType.Defense;
            mainStat.TypeBonus = TypeBonus.FlatBonus;
            mainStat.FlatValue = flatValue;
            mainStat.FlatBaseValue = flatValue;
            mainStat.FlatBonus = defaultBonus.MainStat.FlatBonus;
        }

        private void CreateLegArmorMainStat(EquipStat mainStat, Rarity rarity)
        {
            EquipStatBonus defaultBonus = GetRarityBonus(rarity);
            float percentValue = Random.Range(defaultBonus.MainStat.PercentMinValue, defaultBonus.MainStat.PercentMaxValue);
            mainStat.Stat = EquipStatType.Defense;
            mainStat.TypeBonus = TypeBonus.PercentBonus;
            mainStat.PercentValue = percentValue;
            mainStat.PercentBaseValue = percentValue;
            mainStat.PercentBonus = defaultBonus.MainStat.PercentBonus;
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
            foreach(var rarityBonus in rarityBonusDefault)
            {
                if(rarity == rarityBonus.Rarity) return rarityBonus;
            }

            return null;
        }

        private Rarity GetRarity()
        {
            float rate = 1; //Random.Range(0f, 100f);

            if(rate <= (int)Rarity.Lengendary) return Rarity.Lengendary;
            if(rate <= (int)Rarity.Epic) return Rarity.Epic;
            if(rate <= (int)Rarity.Rare) return Rarity.Rare;
            if(rate <= (int)Rarity.Uncommon) return Rarity.Uncommon;
            if(rate <= (int)Rarity.Common) return Rarity.Common;
            return Rarity.None;
        }
    }
}