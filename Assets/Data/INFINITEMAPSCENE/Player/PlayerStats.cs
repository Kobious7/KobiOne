using System;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;

namespace InfiniteMap
{
    public class PlayerStats : PlayerAb
    {
        [SerializeField] private int level = 1;

        public int Level
        {
            get { return level; }
            set { level = value; }
        }

        [SerializeField] private int requiredExp;

        public int RequiredExp
        {
            get { return requiredExp; }
            set { requiredExp = value; }
        }

        [SerializeField] private int currentExp;

        public int CurrentExp
        {
            get { return currentExp; }
            set { currentExp = value; }
        }

        [SerializeField] private int remainPoints;

        public int RemainPoints
        {
            get { return remainPoints; }
            set { remainPoints = value; }
        }

        //=======Potential======

        [SerializeField] private int power = 1;

        public int Power
        {
            get { return power; }
            set { power = value; }
        }

        [SerializeField] private int magic;

        public int Magic
        {
            get { return magic; }
            set { magic = value; }
        }

        [SerializeField] private int strength;

        public int Strength
        {
            get { return strength; }
            set { strength = value; }
        }

        [SerializeField] private int defense;

        public int Defense
        {
            get { return defense; }
            set { defense = value; }
        }

        [SerializeField] private int dexterity;

        public int Dexterity
        {
            get { return dexterity;}
            set { dexterity = value;}
        }

        [SerializeField] private List<Stat> potential;

        public List<Stat> Potential
        {
            get { return potential; }
            set { potential = value; }
        }

        //=======Potential======

        //=======Main Stats======

        //Main Stat: Attack

        [SerializeField] private int attack;

        public int Attack
        {
            get { return attack; }
            set { attack = value; }
        }

        [SerializeField] private int slashDamage;

        public int SlashDamage
        {
            get { return slashDamage; }
            set { slashDamage = value; }
        }

        //Main Stat: Magic

        [SerializeField] private int magicAttack;

        public int MagicAttack
        {
            get { return magicAttack; }
            set { magicAttack = value; }
        }

        [SerializeField] private int swordrainDamage;

        public int SwordrainDamage
        {
            get { return swordrainDamage; }
            set { swordrainDamage = value; }
        }

        //Main Stats: Max HP

        [SerializeField] private int maxHP;

        public int MaxHP
        {
            get { return maxHP; }
            set { maxHP = value; }
        }

        //Main Stats: Defense

        //Main Stats: Dexterity

        [SerializeField] private List<Stat> stats;
        
        public List<Stat> Stats
        {
            get { return stats; }
            set { stats = value; }
        }

        //=======Main Stats======

        public event Action<int> OnPotentialChange;
        public event Action<int> OnStatChange;
        public event Action<int> OnLevelUp;

        protected override void Start()
        {
            base.Start();
            LoadStats();
        }

        public void LoadStats()
        {
            LoadPotentialFromSO();
            CreateStats();
            StatsCalculation();
        }

        private void ExpUpdate()
        {
            if(Game.Instance.MapData.MapCanLoad)
            {
                if(Game.Instance.MapData.Result == Result.WIN)
                {
                    int exp = UnityEngine.Random.Range(Game.Instance.MapData.MonsterInfo.Level + 4, Game.Instance.MapData.MonsterInfo.Level + 9);

                    Debug.Log("" + exp);

                    currentExp += exp;

                    while(currentExp >= requiredExp)
                    {
                        level++;
                        remainPoints += 5;
                        currentExp -= requiredExp;

                        IncreaseLevel();

                        int averageMonsters = (level / 10) * 10 + level;
                        requiredExp = ((level / 10) * 10 + 10) * averageMonsters;
                    }
                }
            }
        }

        public int CheckRemainPoints(int point)
        {
            return remainPoints == 0 ? 0 : point <= 0 ? 0 : point > remainPoints ? remainPoints : point;
        }

        private void LoadPotentialFromSO()
        {
            CharacterSO characterData = Game.Instance.CharacterData;
            Stat power = new Stat(0, "Power");
            Stat magic = new Stat(1, "Magic");
            Stat strength = new Stat(2, "Strength");
            Stat defense = new Stat(3, "Defense");
            Stat dexterity = new Stat(4, "Dexterity");
            power.TrueValue = characterData.Power.TrueValue;
            magic.TrueValue = characterData.Magic.TrueValue;
            strength.TrueValue = characterData.Strength.TrueValue;
            defense.TrueValue = characterData.Defense.TrueValue;
            dexterity.TrueValue = characterData.Dexterity.TrueValue;
            potential = new List<Stat>{
                power,
                magic,
                strength,
                defense,
                dexterity
            };

            CalculatePower();
            CalculateMagic();
            CalculateStrength();
            CalculateDefense();
            CalculateDexterity();
        }

        private void CreateStats()
        {
            stats = new List<Stat>{
                new Stat(0, "Level"),
                new Stat(1, "Attack"),
                new Stat(2, "Magic Attack"),
                new Stat(3, "HP"),
                new Stat(4, "Slash Damage"),
                new Stat(5, "Swordrain Damage"),
                new Stat(6, "Defense"),
                new Stat(7, "Accuracy"),
                new Stat(8, "Damage Range"),
                new Stat(9, "Speed"),
                new Stat(10, "Crit Rate"),
                new Stat(11, "Crit Damage")
            };
        }

        private void StatsCalculation()
        {
            for(int i = 0; i < stats.Count; i++)
            {
                Calulate(stats[i]);
            }
        }

        private void Calulate(Stat stat)
        {
            switch(stat.Index)
            {
                case 0:
                    CalculateExp();
                    break;
                case 1:
                    CalculateAttackStat();
                    break;
                case 2:
                    CalculateMagicAttackStat();
                    break;
                case 3:
                    CalculateHPStat();
                    break;
                case 6:
                    CalculateDefenseStat();
                    break;
                case 7:
                    CalculateAccuracyStat();
                    break;
                case 9: 
                    CalculateSpeedStat();
                    break;
                case 10:
                    CalculateCritRateStat();
                    break;
                case 11:
                    CalculateCritDamageStat();
                    break;
            }
        }

        private void CalculateExp()
        {
            if(Game.Instance.MapData.MapCanLoad)
            {
                level = Game.Instance.MapData.PlayerInfo.Level;
                currentExp = Game.Instance.MapData.PlayerInfo.CurrentExp;
            }
            else
            {
                level = Game.Instance.CharacterData.Level;
                currentExp = Game.Instance.CharacterData.CurrentExp;
            }
            int averageMonsters = (level / 10) * 10 + level;
            requiredExp = ((level / 10) * 10 + 10) * averageMonsters;
            Stat levelStat = stats[0];
            levelStat.Value = level;

            ExpUpdate();
        }

        private void IncreaseLevel()
        {
            stats[0].Value = level;
            //Game.Instance.CharacterData.Level = level;
            //Game.Instance.CharacterData.CurrentExp = currentExp;

            OnStatChange?.Invoke(0);
        }

        public void IncreasePotentialPoint(int index, int points)
        {
            if(index == 0) IncreasePowerPoint(points);
            if(index == 1) IncreaseMagicPoint(points);
            if(index == 2) IncreaseStrengthPoint(points);
            if(index == 3) IncreaseDefensePoint(points);
            if(index == 4) IncreaseDexterityPoint(points);
        }

        private void IncreasePowerPoint(int points)
        {
            Stat power = potential[0];
            power.TrueValue += points;
            remainPoints -= points;
            //Game.Instance.CharacterData.Power.TrueValue = power.TrueValue;

            CalculatePowerBranch();
        }

        private void IncreaseMagicPoint(int points)
        {
            Stat magic = potential[1];
            magic.TrueValue += points;
            remainPoints -= points;
            //Game.Instance.CharacterData.Magic.TrueValue = magic.TrueValue;

            CalculateMagicBranch();
        }

        private void IncreaseStrengthPoint(int points)
        {
            Stat strength = potential[2];
            strength.TrueValue += points;
            remainPoints -= points;
            //Game.Instance.CharacterData.Strength.TrueValue = strength.TrueValue;

            CalculateStrengthBranch();
        }

        private void IncreaseDefensePoint(int points)
        {
            Stat defense = potential[3];
            defense.TrueValue += points;
            remainPoints -= points;
            //Game.Instance.CharacterData.Defense.TrueValue = defense.TrueValue;

            CalculateDefenseBranch();
        }

        private void IncreaseDexterityPoint(int points)
        {
            Stat dexterity = potential[4];
            dexterity.TrueValue += points;
            remainPoints -= points;
            //Game.Instance.CharacterData.Dexterity.TrueValue = dexterity.TrueValue;

            CalculateDexterityBranch();
        }

        private void ReCalculate()
        {
            CalculatePowerBranch();
            CalculateMagicBranch();
            CalculateStrengthBranch();
            CalculateDefenseBranch();
            CalculateDexterityBranch();
        }

        private void CalculatePowerBranch()
        {
            CalculatePower();
            CalculateAttackStat();
            OnPotentialChange?.Invoke(0);
            OnStatChange?.Invoke(1);
            OnStatChange?.Invoke(4);
        }

        private void CalculateMagicBranch()
        {
            CalculateMagic();
            CalculateMagicAttackStat();
            OnPotentialChange?.Invoke(1);
            OnStatChange?.Invoke(2);
            OnStatChange?.Invoke(5);
        }

        private void CalculateStrengthBranch()
        {
            CalculateStrength();
            CalculateHPStat();
            OnPotentialChange?.Invoke(2);
            OnStatChange?.Invoke(3);
        }

        private void CalculateDefenseBranch()
        {
            CalculateDefense();
            CalculateDefenseStat();
            OnPotentialChange?.Invoke(3);
            OnStatChange?.Invoke(6);
        }

        private void CalculateDexterityBranch()
        {
            CalculateDexterity();
            CalculateAccuracyStat();
            OnPotentialChange?.Invoke(4);
            OnStatChange?.Invoke(7);
            OnStatChange?.Invoke(8);
        }

        private void CalculateOtherSources()
        {
            OnStatChange?.Invoke(10);
            OnStatChange?.Invoke(11);
        }

        private void CalculatePower()
        {
            Stat power = potential[0];
            int flat = power.TrueValue + power.FlatBonus;
            Debug.Log(flat);
            Debug.Log(flat * power.PercentBonus);
            power.Value = (int) (flat + flat * power.PercentBonus / 100); 
        }

        private void CalculateMagic()
        {
            Stat magic = potential[1];
            int flat = magic.TrueValue + magic.FlatBonus;
            magic.Value = (int) (flat + flat * magic.PercentBonus / 100);
        }

        private void CalculateStrength()
        {
            Stat strength = potential[2];
            int flat = strength.TrueValue + strength.FlatBonus;
            strength.Value = (int) (flat + flat * strength.PercentBonus / 100);
        }

        private void CalculateDefense()
        {
            Stat defense = potential[3];
            int flat = defense.TrueValue + defense.FlatBonus;
            defense.Value = (int) (flat + flat * defense.PercentBonus / 100);
        }

        private void CalculateDexterity()
        {
            Stat dexterity = potential[4];
            int flat = dexterity.TrueValue + dexterity.FlatBonus;
            dexterity.Value = (int) (flat + flat * dexterity.PercentBonus / 100);
        }

        private void CalculateAttackStat()
        {
            Stat power = potential[0];
            Stat attack = stats[1];
            int flat = power.Value / 5 + attack.FlatBonus;
            attack.Value = (int) (flat + flat * attack.PercentBonus / 100);

            CalculateSlashDamageStat();
        }

        private void CalculateSlashDamageStat()
        {
            Stat attack = stats[1];
            Stat slashDamage = stats[4];
            slashDamage.Value = (int) (attack.Value * 1.1);
        }

        private void CalculateMagicAttackStat()
        {
            Stat magic = potential[1];
            Stat magicAttack = stats[2];
            int flat = magic.Value / 5 + magicAttack.FlatBonus;
            magicAttack.Value = (int) (flat + flat * magicAttack.PercentBonus/ 100);

            CalculateSwordrainDamageStat();
        }

        private void CalculateSwordrainDamageStat()
        {
            Stat magicAttack = stats[2];
            Stat swordrainDamage = stats[5];
            swordrainDamage.Value = (int) (magicAttack.Value * 1.1);
        }

        private void CalculateHPStat()
        {
            Stat strength = potential[2];
            Stat hp = stats[3];
            int flat = strength.Value * 10 + hp.FlatBonus;
            hp.Value = (int) (flat + flat * hp.PercentBonus/ 100);
        }

        private void CalculateDefenseStat()
        {
            Stat defense = potential[3];
            Stat def = stats[6];
            int flat = defense.Value / 5 + defense.FlatBonus;
            def.Value = (int) (flat + flat * def.PercentBonus / 100);
        }

        private void CalculateAccuracyStat()
        {
            Stat dexterity = potential[4];
            Stat accuracy = stats[7];
            int flat = dexterity.Value / 20 + accuracy.FlatBonus;
            accuracy.Value = (int) (flat + flat * accuracy.PercentBonus / 100);

            CalculateDamageRangeStat();
        }

        private void CalculateDamageRangeStat()
        {
            Stat accuracy = stats[7];
            Stat damageRange = stats[8];
            damageRange.PercentBonus = -20 + accuracy.Value/1000;
            damageRange.IsPercentValue = true;
        }

        private void CalculateSpeedStat()
        {
            Stat speed = stats[9];
            int flat = 4 + speed.FlatBonus;
            speed.Value = (int) flat;
        }

        private void CalculateCritRateStat()
        {
            Stat critRate = stats[10];
            critRate.IsPercentValue = true;
        }

        private void CalculateCritDamageStat()
        {
            Stat critDamage = stats[11];
            critDamage.IsPercentValue = true;
        }

        public void UpdateBonus(List<EquipBonus> equipBonus)
        {
            foreach(var bonus in equipBonus)
            {
                int index = GetIndexFromStat(bonus);

                if(bonus.Stat == EquipStatType.Power || bonus.Stat == EquipStatType.Magic || bonus.Stat == EquipStatType.Strength
                    || bonus.Stat == EquipStatType.DefenseP || bonus.Stat == EquipStatType.Dexterity)
                {
                    potential[index].FlatBonus = bonus.FlatValue;
                    potential[index].PercentBonus = bonus.PercentValue;
                }
                else
                {
                    stats[index].FlatBonus = bonus.FlatValue;
                    stats[index].PercentBonus = bonus.PercentValue;
                }
            }

            ReCalculate();
        }

        private int GetIndexFromStat(EquipBonus equipBonus)
        {
            if(equipBonus.Stat == EquipStatType.Power) return 0;
            if(equipBonus.Stat == EquipStatType.Magic) return 1;
            if(equipBonus.Stat == EquipStatType.Strength) return 2;
            if(equipBonus.Stat == EquipStatType.DefenseP) return 3;
            if(equipBonus.Stat == EquipStatType.Dexterity) return 4;
            if(equipBonus.Stat == EquipStatType.Attack) return 1;
            if(equipBonus.Stat == EquipStatType.MagicAttack) return 2;
            if(equipBonus.Stat == EquipStatType.HP) return 3;
            if(equipBonus.Stat == EquipStatType.Defense) return 6;
            if(equipBonus.Stat == EquipStatType.Accuracy) return 7;
            if(equipBonus.Stat == EquipStatType.DamageRange) return 8;
            if(equipBonus.Stat == EquipStatType.Speed) return 9;
            if(equipBonus.Stat == EquipStatType.CritRate) return 10;
            if(equipBonus.Stat == EquipStatType.CritDamage) return 11;
            return 1000000;
        }
    }
}