using System;
using System.Collections.Generic;
using UnityEngine;

public class IMPlayerStats : EntityComponent
{
    [Header("EXP & Level")]
    [SerializeField] private int level = 1, requiredExp, currentExp, lerpExpStack, expFromBattle;

    [Header("Stat Points")]
    [SerializeField] private int remainPoints;

    [Header("Stats")]
    [SerializeField] private List<Stat> potential, stats,  equipPotentialBonus, equipStatBonus, passiveSkillPotentialBonus, passiveSkillStatBonus;

    private string[] potentialNames = { "Power", "Magic", "Strength", "Defense", "Dexterity" };
    private string[] statNames = { "Level", "Attack", "Magic Attack", "HP", "Slash Damage", "Swordrain Damage", "Defense", "Accuracy", "Damage Range", "Speed",
                                "Crit Rate", "Crit Damage", "Mana Regeneration" };

    public event Action<int> OnPotentialChange;
    public event Action<int> OnStatChange;

    private InfiniteMapManager infiniteMapManager;
    private SkillUpdateBonus skillUpdateBonus;
    private EquipmentCalculator equipmentCalculator;

    #region Properties
    public int Level { get => level; set => level = value; }
    public int RequiredExp { get => requiredExp; set => requiredExp = value; }
    public int CurrentExp { get => currentExp; set => currentExp = value; }
    public int RemainPoints { get => remainPoints; set => remainPoints = value; }
    public List<Stat> Potential { get => potential; set => potential = value; }
    public List<Stat> Stats { get => stats; set => stats = value; }
    public int LerpExpStack { get => lerpExpStack; set => lerpExpStack = value; }
    public int ExpFromBattle { get => expFromBattle; set => expFromBattle = value; }
    #endregion

    protected override void Start()
    {
        base.Start();

        infiniteMapManager = InfiniteMapManager.Instance;
        infiniteMapManager.Skill.OnSkillPointsReset += UpdatePassiveSkillBonus;

        skillUpdateBonus = infiniteMapManager.Skill.BonusUpdating;
        skillUpdateBonus.OnSkillBonusChanged += UpdatePassiveSkillBonus;

        equipmentCalculator = infiniteMapManager.Equipment.Calculator;
        equipmentCalculator.OnTotalBonusChanged += UpdateEquipBonus;

        LoadStats();
        UpdatePassiveSkillBonus(skillUpdateBonus.InitActiveSkillTreesBonus());
        equipmentCalculator.InitTotalBonus();
    }

    public void LoadStats()
    {
        LoadPotentialFromSO();
        CreateStatLists();
        CalculateExp();
        StatsCalculation();
    }

    public int CheckRemainPoints(int point)
    {
        return remainPoints == 0 ? 0 : point <= 0 ? 0 : point > remainPoints ? remainPoints : point;
    }

    private void LoadPotentialFromSO()
    {
        CharacterSO characterData = infiniteMapManager.CharacterData;

        potential = CreateStatListFromNames(potentialNames);
        potential[0].TrueValue = characterData.Power.TrueValue;
        potential[1].TrueValue = characterData.Magic.TrueValue;
        potential[2].TrueValue = characterData.Strength.TrueValue;
        potential[3].TrueValue = characterData.Defense.TrueValue;
        potential[4].TrueValue = characterData.Dexterity.TrueValue;

        CalculatePower();
        CalculateMagic();
        CalculateStrength();
        CalculateDefense();
        CalculateDexterity();
    }

    private void CreateStatLists()
    {
        stats = CreateStatListFromNames(statNames);
        equipPotentialBonus = CreateStatListFromNames(potentialNames);
        equipStatBonus = CreateStatListFromNames(statNames);
        passiveSkillPotentialBonus = CreateStatListFromNames(potentialNames);
        passiveSkillStatBonus = CreateStatListFromNames(statNames);
    }

    private List<Stat> CreateStatListFromNames(string[] names)
    {
        List<Stat> statList = new List<Stat>();

        for (int i = 0; i < names.Length; i++)
        {
            statList.Add(new Stat(i, names[i]));
        }

        return statList;
    }

    private void StatsCalculation()
    {
        for (int i = 0; i < stats.Count; i++)
        {
            Calulate(stats[i]);
        }
    }

    private void Calulate(Stat stat)
    {
        switch(stat.Index)
        {
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

        requiredExp = CalculateRequiredExp(level);
        Stat levelStat = stats[0];
        levelStat.Value = level;

        ExpUpdate();
    }

    private void ExpUpdate()
    {
        if(infiniteMapManager.MapData.MapCanLoad)
        {
            int exp = infiniteMapManager.MapData.Result == Result.WIN ?
                        UnityEngine.Random.Range(infiniteMapManager.MapData.MonsterInfo.Level + 4, infiniteMapManager.MapData.MonsterInfo.Level + 9) : 0;

            expFromBattle = exp + infiniteMapManager.MapData.PlayerInfo.ExpFromBattle;
            currentExp += expFromBattle;

            while (currentExp >= requiredExp)
            {
                level++;
                lerpExpStack++;
                remainPoints += 5;
                currentExp -= requiredExp;

                IncreaseLevel();

                requiredExp = CalculateRequiredExp(level);
            }
        }
    }

    private int CalculateRequiredExp(int level)
    {
        int avgMonster = (level / 10) * 10 + level;
        return ((level / 10) * 10 + 10) * avgMonster;
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

    #region Calculate Potentials & Stats
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
    #endregion

    public void UpdatePassiveSkillBonus(List<OtherSourcesBonus> passiveSkillBonus)
    {
        ApplyBonusList(passiveSkillBonus, passiveSkillPotentialBonus, passiveSkillStatBonus);
    }

    public void UpdateEquipBonus(List<OtherSourcesBonus> equipBonus)
    {
        ApplyBonusList(equipBonus, equipPotentialBonus, equipStatBonus);
    }

    private void ApplyBonusList<T>(List<T> list, List<Stat> potentialList, List<Stat> statList) where T : OtherSourcesBonus
    {
        foreach (var bonus in list)
        {
            int index = GetIndexFromStat(bonus.Stat);
            if (index == -1) continue;

            if (bonus.Stat == EquipStatType.Power || bonus.Stat == EquipStatType.Magic || bonus.Stat == EquipStatType.Strength || bonus.Stat == EquipStatType.DefenseP
                || bonus.Stat == EquipStatType.Dexterity)
            {
                potentialList[index].FlatBonus = bonus.FlatValue;
                potentialList[index].PercentBonus = bonus.PercentValue;
            }
            else
            {
                statList[index].FlatBonus = bonus.FlatValue;
                statList[index].PercentBonus = bonus.PercentValue;
            }
        }
        UpdateBonus();
    }

    public void UpdateBonus()
    {
        foreach (var potential in potential)
        {
            potential.FlatBonus = 0;
            potential.PercentBonus = 0;
        }

        foreach (var stat in stats)
        {
            stat.FlatBonus = 0;
            stat.PercentBonus = 0;
        }

        int i = 0;

        foreach (var potential in potential)
        {
            potential.FlatBonus += equipPotentialBonus[i].FlatBonus + passiveSkillPotentialBonus[i].FlatBonus;
            potential.PercentBonus += equipPotentialBonus[i].PercentBonus + passiveSkillPotentialBonus[i].PercentBonus;
            i++;
        }

        i = 0;

        foreach (var stat in stats)
        {
            stat.FlatBonus += equipStatBonus[i].FlatBonus + passiveSkillStatBonus[i].FlatBonus;
            stat.PercentBonus += equipStatBonus[i].PercentBonus + passiveSkillStatBonus[i].PercentBonus;
            i++;
        }

        ReCalculate();
    }
    
    private int GetIndexFromStat(EquipStatType stat)
    {
        return stat switch
        {
            EquipStatType.Power => 0,
            EquipStatType.Magic => 1,
            EquipStatType.Strength => 2,
            EquipStatType.DefenseP => 3,
            EquipStatType.Dexterity => 4,
            EquipStatType.Attack => 1,
            EquipStatType.MagicAttack => 2,
            EquipStatType.HP => 3,
            EquipStatType.Defense => 6,
            EquipStatType.Accuracy => 7,
            EquipStatType.DamageRange => 8,
            EquipStatType.Speed => 9,
            EquipStatType.CritRate => 10,
            EquipStatType.CritDamage => 11,
            EquipStatType.ManaRegen => 12,
            _ => -1
        };
    }
}