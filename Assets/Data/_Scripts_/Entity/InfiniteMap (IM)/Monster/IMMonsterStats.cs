using System.Runtime.CompilerServices;
using UnityEngine;

public class IMMonsterStats : EntityComponent
{
    [SerializeField] private int zeroLevel;
    [SerializeField] private int level;
    [SerializeField] private AttackType attackType;
    [SerializeField] private MonsterTier tier;
    [SerializeField] private int power;
    [SerializeField] private int magic;
    [SerializeField] private int strength;
    [SerializeField] private int defense;
    [SerializeField] private int dexterity;
    [SerializeField] private int attack;
    [SerializeField] private int magicAttack;
    [SerializeField] private int hp;
    [SerializeField] private int slashDamage;
    [SerializeField] private int swordrainDamage;
    [SerializeField] private int defenseS;
    [SerializeField] private int accuracy;
    [SerializeField] private int damageRange;
    [SerializeField] private float critRate;
    [SerializeField] private float critDamage;
    [SerializeField] private int manaRegen;
    [SerializeField] private bool loadFromData;

    #region Properties
    public int ZeroLevel { get => zeroLevel; set => zeroLevel = value; }
    public int Level { get => level; set => level = value; }
    public AttackType AttackType { get => attackType; set => attackType = value; }
    public MonsterTier Tier { get => tier; set => tier = value; }
    public int Power { get => power; set => power = value; }
    public int Magic { get => magic; set => magic = value; }
    public int Strength { get => strength; set => strength = value; }
    public int Defense { get => defense; set => defense = value; }
    public int Dexterity { get => dexterity; set => dexterity = value; }
    public int Attack { get => attack; set => attack = value; }
    public int MagicAttack { get => magicAttack; set => magicAttack = value; }
    public int HP { get => hp; set => hp = value; }
    public int SlashDamage { get => slashDamage; set => slashDamage = value; }
    public int SwordrainDamage { get => swordrainDamage; set => swordrainDamage = value; }
    public int DefenseS { get => defenseS; set => defenseS = value; }
    public int Accuracy { get => accuracy; set => accuracy = value; }
    public int DamageRange { get => damageRange; set => damageRange = value; }
    public float CritRate { get => critRate; set => critRate = value; }
    public float CritDamage { get => critDamage; set => critDamage = value; }
    public int ManaRegen { get => manaRegen; set => manaRegen = value; }
    public bool LoadFromData { get => loadFromData; set => loadFromData = value; }
    #endregion

    protected override void OnEnable()
    {
        base.OnEnable();

        if (loadFromData)
        {
            level = zeroLevel + 10 * InfiniteMapManager.Instance.MapData.MapInfo.Level;
            loadFromData = false;
        }
        else
        {
            level = zeroLevel + 10 * InfiniteMapManager.Instance.Map.MapLevel.CurrentLevel;
        }
    }

    public void CalculateStats()
    {
        CalculatePower();
        CalculateMagic();
        CalculateStrength();
        CalculateDefense();
        CalculateDexterity();
        CalculateAttack();
        CalculateMagicAttack();
        CalculateHP();
        CalculateSlashDamage();
        CalculateSwordrainDamage();
        CalculateDefenseS();
        CalculateAccuracy();
        CalculateDamageRange();
        CalculateCritRate();
        CalculateCritDamage();
        CalculateManaRegen();
    }

    #region Stats calculation
    private void CalculatePower()
    {
        if (attackType == AttackType.Attack)
        {
            power = level > 9 ? level * 5 : (int)(level * 1.5f);
        }

        if (attackType == AttackType.MagicAttack)
        {
            power = level > 9 ? Random.Range(1, (int)(level * 2.5)) : 0;
        }

        if (attackType == AttackType.AllAttack)
        {
            power = level > 9 ? level * 3 : level;
        }
    }

    private void CalculateMagic()
    {
        if (attackType == AttackType.Attack)
        {
            magic = level > 9 ? Random.Range(1, (int)(level * 2.5)) : 0;
        }

        if (attackType == AttackType.MagicAttack)
        {
            magic = level > 9 ? level * 5 : (int)(level * 1.5f);
        }

        if (attackType == AttackType.AllAttack)
        {
            magic = level > 9 ? level * 3 : level;
        }
    }

    private void CalculateStrength()
    {
        strength = level * 5;
    }

    private void CalculateDefense()
    {
        defense = level >= 10 ? level * 5 : 0;
    }

    private void CalculateDexterity()
    {
        dexterity = level * 5;
    }

    private void CalculateAttack()
    {
        float attackBasicMultiplier = 0.2f;
        float multiplierPerPower = 0.001f;
        float multiplier = power * multiplierPerPower + attackBasicMultiplier;
        attack = power + (int)(power * multiplier);
    }

    private void CalculateMagicAttack()
    {
        float magicAttackBasicMultiplier = 0.2f;
        float multiplierPerMagic = 0.001f;
        float multiplier = magic * multiplierPerMagic + magicAttackBasicMultiplier;
        magicAttack = magic + (int)(magic * multiplier);
    }

    private void CalculateHP()
    {
        float strengthBasicMultiplier = 1f;
        float multiplierPerStrength = 0.01f;
        float multiplier = strength * multiplierPerStrength + strengthBasicMultiplier;
        hp = strength + (level > 9 ? (int)(strength * multiplier) * 5 : 20);
        hp = (int)(hp * GetTierHPMultiplier(tier));
    }

    private void CalculateSlashDamage()
    {
        slashDamage = (int)(attack * 0.3f);

        if (attackType == AttackType.Attack || attackType == AttackType.AllAttack)
            slashDamage = (int)(slashDamage * GetTierDamageMultiplier(tier));
    }

    private void CalculateSwordrainDamage()
    {
        swordrainDamage = (int)(magicAttack * 0.3f);

        if (attackType == AttackType.MagicAttack || attackType == AttackType.AllAttack)
            swordrainDamage = (int)(swordrainDamage * GetTierDamageMultiplier(tier));
    }

    private void CalculateDefenseS()
    {
        defenseS = defense / 5;
    }

    private void CalculateAccuracy()
    {
        accuracy = (int)(dexterity / 20);
    }

    private void CalculateDamageRange()
    {
        int basicDamageRange = -20;

        damageRange = basicDamageRange + accuracy / 1000;
    }

    private void CalculateCritRate()
    {
        if (level >= 100)
        {
            critRate = Random.Range(50f, 70f);
        }
        else if (level >= 70f)
        {
            critRate = Random.Range(40f, 60f);
        }
        else if (level >= 40f)
        {
            critRate = Random.Range(30f, 50f);
        }
        else if (level >= 20f)
        {
            critRate = Random.Range(10f, 30f);
        }
        else if (level >= 10)
        {
            critRate = Random.Range(0f, 20f);
        }
        else
        {
            critRate = 0;
        }
    }

    private void CalculateCritDamage()
    {
        if (level >= 100)
        {
            CritDamage = Random.Range(100f, 200f);
        }
        else if (level >= 50f)
        {
            CritDamage = Random.Range(90f, 150f);
        }
        else if (level >= 20)
        {
            CritDamage = Random.Range(50f, 100f);
        }
        else
        {
            CritDamage = Random.Range(0f, 70f);
        }
    }

    private void CalculateManaRegen()
    {
        if (level >= 100)
        {
            manaRegen = 3;
        }
        else if (level >= 50)
        {
            manaRegen = 2;
        }
        else
        {
            manaRegen = 1;
        }
    }

    private float GetTierDamageMultiplier(MonsterTier mTier)
    {
        return mTier switch
        {
            MonsterTier.Elite => 1.5f,
            MonsterTier.Rampage => 2f,
            MonsterTier.Boss => 10f,
            _ => 1f
        };
    }

    private float GetTierHPMultiplier(MonsterTier mTier)
    {
        return mTier switch
        {
            MonsterTier.Elite => 2f,
            MonsterTier.Rampage => 5f,
            MonsterTier.Boss => 150f,
            _ => 1f
        };
    }
    #endregion
}