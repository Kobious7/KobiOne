using System;
using System.Collections.Generic;
using UnityEngine;

public class IMPlayerStats : EntityComponent
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

    [SerializeField] private List<Stat> equipPotentialBonus;
    [SerializeField] private List<Stat> equipStatBonus;
    [SerializeField] private List<Stat> passiveSkillPotentialBonus;
    [SerializeField] private List<Stat> passiveSkillStatBonus;
    public event Action<int> OnPotentialChange;
    public event Action<int> OnStatChange;
    public event Action<int> OnLevelUp;
    private InfiniteMapManager infiniteMapManager;

    protected override void Start()
    {
        base.Start();

        infiniteMapManager = InfiniteMapManager.Instance;

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
        if(infiniteMapManager.MapData.MapCanLoad)
        {
            if(infiniteMapManager.MapData.Result == Result.WIN)
            {
                int exp = UnityEngine.Random.Range(infiniteMapManager.MapData.MonsterInfo.Level + 4, infiniteMapManager.MapData.MonsterInfo.Level + 9);

                //Debug.Log("" + exp);

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
        CharacterSO characterData = InfiniteMapManager.Instance.CharacterData;
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
        potential.Clear();
        potential.Add(power);
        potential.Add(magic);
        potential.Add(strength);
        potential.Add(defense);
        potential.Add(dexterity);

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
            new Stat(11, "Crit Damage"),
            new Stat(12, "Mana Regeneration")
        };

        equipPotentialBonus = new List<Stat>
        {
            new Stat(0, "Power"),
            new Stat(1, "Magic"),
            new Stat(2, "Strength"),
            new Stat(3, "Defense"),
            new Stat(4, "Dexterity")
        };

        equipStatBonus = new List<Stat>{
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
            new Stat(11, "Crit Damage"),
            new Stat(12, "Mana Regeneration")
        };

        passiveSkillPotentialBonus = new List<Stat>
        {
            new Stat(0, "Power"),
            new Stat(1, "Magic"),
            new Stat(2, "Strength"),
            new Stat(3, "Defense"),
            new Stat(4, "Dexterity")
        };

        passiveSkillStatBonus = new List<Stat>{
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
            new Stat(11, "Crit Damage"),
            new Stat(12, "Mana Regeneration")
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
            case 12:
                CalculateManaRegenStat();
                break;
        }
    }

    private void CalculateExp()
    {
        if(infiniteMapManager.MapData.MapCanLoad)
        {
            level = infiniteMapManager.MapData.PlayerInfo.Level;
            currentExp = infiniteMapManager.MapData.PlayerInfo.CurrentExp;
        }
        else
        {
            level = infiniteMapManager.CharacterData.Level;
            currentExp = infiniteMapManager.CharacterData.CurrentExp;
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
        //infiniteMapManager.CharacterData.Level = level;
        //infiniteMapManager.CharacterData.CurrentExp = currentExp;

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
        //infiniteMapManager.CharacterData.Power.TrueValue = power.TrueValue;

        CalculatePowerBranch();
    }

    private void IncreaseMagicPoint(int points)
    {
        Stat magic = potential[1];
        magic.TrueValue += points;
        remainPoints -= points;
        //infiniteMapManager.CharacterData.Magic.TrueValue = magic.TrueValue;

        CalculateMagicBranch();
    }

    private void IncreaseStrengthPoint(int points)
    {
        Stat strength = potential[2];
        strength.TrueValue += points;
        remainPoints -= points;
        //infiniteMapManager.CharacterData.Strength.TrueValue = strength.TrueValue;

        CalculateStrengthBranch();
    }

    private void IncreaseDefensePoint(int points)
    {
        Stat defense = potential[3];
        defense.TrueValue += points;
        remainPoints -= points;
        //infiniteMapManager.CharacterData.Defense.TrueValue = defense.TrueValue;

        CalculateDefenseBranch();
    }

    private void IncreaseDexterityPoint(int points)
    {
        Stat dexterity = potential[4];
        dexterity.TrueValue += points;
        remainPoints -= points;
        //infiniteMapManager.CharacterData.Dexterity.TrueValue = dexterity.TrueValue;

        CalculateDexterityBranch();
    }

    private void ReCalculate()
    {
        CalculatePowerBranch();
        CalculateMagicBranch();
        CalculateStrengthBranch();
        CalculateDefenseBranch();
        CalculateDexterityBranch();
        CalculateOtherSources();
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
        float multiplier = power.Value * 0.0001f + 0.2f;
        float flat = power.Value * multiplier + attack.FlatBonus;
        attack.Value = (int) (flat + flat * attack.PercentBonus / 100);

        CalculateSlashDamageStat();
    }

    private void CalculateSlashDamageStat()
    {
        Stat attack = stats[1];
        Stat slashDamage = stats[4];
        slashDamage.Value = (int) (attack.Value * 0.3f) < 1 ? 1 : (int) (attack.Value * 0.3f);
    }

    private void CalculateMagicAttackStat()
    {
        Stat magic = potential[1];
        Stat magicAttack = stats[2];
        float multiplier = magic.Value * 0.0001f + 0.2f;
        float flat = magic.Value * multiplier + magicAttack.FlatBonus;
        magicAttack.Value = (int) (flat + flat * magicAttack.PercentBonus/ 100);

        CalculateSwordrainDamageStat();
    }

    private void CalculateSwordrainDamageStat()
    {
        Stat magicAttack = stats[2];
        Stat swordrainDamage = stats[5];
        swordrainDamage.Value = (int) (magicAttack.Value * 0.3f) < 1 ? 1 : (int) (magicAttack.Value * 0.3f);
    }

    private void CalculateHPStat()
    {
        Stat strength = potential[2];
        Stat hp = stats[3];
        float multiplier = 1 + strength.Value * 0.0005f;
        float flat = strength.Value * multiplier;
        hp.Value = (int) ((flat + flat * hp.PercentBonus / 100) * 5) ;
    }

    private void CalculateDefenseStat()
    {
        Stat defense = potential[3];
        Stat def = stats[6];
        int flat = defense.Value / 5 + def.FlatBonus;
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

    private void CalculateManaRegenStat()
    {
        Stat manaRegen = stats[12];
        manaRegen.PercentBonus = 1;
        manaRegen.IsPercentValue = true;
    }

    public void UpdatePassiveSkillBonus(List<PassiveSkillBonus> passiveSkillBonus)
    {
        foreach(var bonus in passiveSkillBonus)
        {
            int index = GetIndexFromStat(bonus);

            if(bonus.Stat == EquipStatType.Power || bonus.Stat == EquipStatType.Magic || bonus.Stat == EquipStatType.Strength
                || bonus.Stat == EquipStatType.DefenseP || bonus.Stat == EquipStatType.Dexterity)
            {
                passiveSkillPotentialBonus[index].FlatBonus = bonus.FlatValue;
                passiveSkillPotentialBonus[index].PercentBonus = bonus.PercentValue;
            }
            else
            {
                passiveSkillStatBonus[index].FlatBonus = bonus.FlatValue;
                passiveSkillStatBonus[index].PercentBonus = bonus.PercentValue;
            }
        }

        UpdateBonus();
    }

    public void UpdateEquipBonus(List<EquipBonus> equipBonus)
    {
        foreach(var bonus in equipBonus)
        {
            int index = GetIndexFromStat(bonus);

            if(bonus.Stat == EquipStatType.Power || bonus.Stat == EquipStatType.Magic || bonus.Stat == EquipStatType.Strength
                || bonus.Stat == EquipStatType.DefenseP || bonus.Stat == EquipStatType.Dexterity)
            {
                equipPotentialBonus[index].FlatBonus = bonus.FlatValue;
                equipPotentialBonus[index].PercentBonus = bonus.PercentValue;
            }
            else
            {
                equipStatBonus[index].FlatBonus = bonus.FlatValue;
                equipStatBonus[index].PercentBonus = bonus.PercentValue;
            }
        }

        UpdateBonus();
    }

    public void UpdateBonus()
    {
        foreach(var potential in potential)
        {
            potential.FlatBonus = 0;
            potential.PercentBonus = 0;
        }

        foreach(var stat in stats)
        {
            stat.FlatBonus = 0;
            stat.PercentBonus = 0;
        }

        int i = 0;

        foreach(var potential in potential)
        {
            potential.FlatBonus += equipPotentialBonus[i].FlatBonus + passiveSkillPotentialBonus[i].FlatBonus;
            potential.PercentBonus += equipPotentialBonus[i].PercentBonus + passiveSkillPotentialBonus[i].PercentBonus;
            i++;
        }

        i = 0;

        foreach(var stat in stats)
        {
            stat.FlatBonus += equipStatBonus[i].FlatBonus + passiveSkillStatBonus[i].FlatBonus;
            stat.PercentBonus += equipStatBonus[i].PercentBonus + passiveSkillStatBonus[i].PercentBonus;
            i++;
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
        if(equipBonus.Stat == EquipStatType.ManaRegen) return 12;
        return 1000000;
    }

    private int GetIndexFromStat(PassiveSkillBonus passiveSkillBonus)
    {
        if(passiveSkillBonus.Stat == EquipStatType.Power) return 0;
        if(passiveSkillBonus.Stat == EquipStatType.Magic) return 1;
        if(passiveSkillBonus.Stat == EquipStatType.Strength) return 2;
        if(passiveSkillBonus.Stat == EquipStatType.DefenseP) return 3;
        if(passiveSkillBonus.Stat == EquipStatType.Dexterity) return 4;
        if(passiveSkillBonus.Stat == EquipStatType.Attack) return 1;
        if(passiveSkillBonus.Stat == EquipStatType.MagicAttack) return 2;
        if(passiveSkillBonus.Stat == EquipStatType.HP) return 3;
        if(passiveSkillBonus.Stat == EquipStatType.Defense) return 6;
        if(passiveSkillBonus.Stat == EquipStatType.Accuracy) return 7;
        if(passiveSkillBonus.Stat == EquipStatType.DamageRange) return 8;
        if(passiveSkillBonus.Stat == EquipStatType.Speed) return 9;
        if(passiveSkillBonus.Stat == EquipStatType.CritRate) return 10;
        if(passiveSkillBonus.Stat == EquipStatType.CritDamage) return 11;
        if(passiveSkillBonus.Stat == EquipStatType.ManaRegen) return 12;
        return 1000000;
    }
}