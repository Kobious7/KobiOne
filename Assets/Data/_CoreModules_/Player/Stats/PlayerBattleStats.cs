using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleStats : PlayerComponent, IEntityBattleStats
{
    [SerializeField] protected int attack;

    public int Attack
    {
        get { return attack; }
        set { attack = value; }
    }

    [SerializeField] protected int magicAttack;

    public int MagicAttack
    {
        get { return magicAttack; }
        set { magicAttack = value; }
    }

    [SerializeField] protected int currentHP = 0;

    public int CurrentHP
    {
        get { return currentHP; }
        set { currentHP = value; }
    }

    [SerializeField] protected int maxHP = 50;

    public int MaxHP
    {
        get { return maxHP; }
        set { maxHP = value; }
    }

    [SerializeField] protected int defense;

    public int Defense
    {
        get { return defense; }
        set { defense = value; }
    }

    [SerializeField] protected int accuracy;

    public int Accuracy
    {
        get { return accuracy; }
        set { accuracy = value; }
    }

    [SerializeField] protected int slashDamage = 2;

    public int SlashDamage
    {
        get { return slashDamage; }
        set { slashDamage = value; }
    }

    [SerializeField] protected int swordrainDamage = 1;

    public int SwordrainDamage
    {
        get { return swordrainDamage; }
        set { swordrainDamage = value; }
    }

    [SerializeField] protected float damageRange;

    public float DamageRange
    {
        get { return damageRange; }
        set { damageRange = value; }
    }

    [SerializeField] protected float critRate;

    public float CritRate
    {
        get { return critRate; }
        set { critRate = value; }
    }

    [SerializeField] protected float critDamage;

    public float CritDamage
    {
        get { return critDamage; }
        set { critDamage = value; }
    }

    [SerializeField] protected float manaRegen;

    public float ManaRegen
    {
        get { return manaRegen;}
        set { manaRegen = value;}
    }

    [SerializeField] protected int vHP = 0;

    public int VHP
    {
        get { return vHP; }
        set { vHP = value; }
    }

    [SerializeField] protected int mana = 0;

    public int Mana
    {
        get { return mana; }
        set { mana = value; }
    }

    [SerializeField] protected int shieldCount = 0;

    public int ShieldCount
    {
        get { return shieldCount; }
        set { shieldCount = value; }
    }

    [SerializeField] protected int shieldStack = 0;

    public int ShieldStack
    {
        get { return shieldStack; }
        set { shieldStack = value; }
    }

    [SerializeField] protected Dictionary<EquipStatType, StatBuffInfo> buffPercents;

    public Dictionary<EquipStatType, StatBuffInfo> BuffPercents
    {
        get { return buffPercents; }
        set { buffPercents = value; }
    }

    protected override void Start()
    {
        base.Start();
        
        PlayerInfo playerInfo = Game.Instance.MapData.PlayerInfo;

        if (Game.Instance.MapData.MapCanLoad)
        {
            attack = playerInfo.Attack;
            magicAttack = playerInfo.MagicAttack;
            maxHP = playerInfo.HP;
            currentHP = maxHP;
            swordrainDamage = playerInfo.SwordrainDamage;
            slashDamage = playerInfo.SlashDamage;
            defense = playerInfo.Defense;
            accuracy = playerInfo.Accuracy;
            damageRange = playerInfo.DamageRange;
            critRate = playerInfo.CritRate;
            critDamage = playerInfo.CritDamage;
            manaRegen = playerInfo.ManaRegen;
        }

        InitBuff(); 
    }

    protected void InitBuff()
    {
        buffPercents = new();
        buffPercents[EquipStatType.Attack] = new StatBuffInfo(attack, 0, 0);
        buffPercents[EquipStatType.MagicAttack] = new StatBuffInfo(magicAttack, 0, 0);
        buffPercents[EquipStatType.CurrentHPByMaxHP] = new StatBuffInfo(maxHP, 0, 0);
        buffPercents[EquipStatType.HP] = new StatBuffInfo(maxHP, 0, 0);
        buffPercents[EquipStatType.Defense] = new StatBuffInfo(defense, 0, 0);
        buffPercents[EquipStatType.Accuracy] = new StatBuffInfo(accuracy, 0, 0);
        buffPercents[EquipStatType.SlashDamage] = new StatBuffInfo(slashDamage, 0, 0);
        buffPercents[EquipStatType.SwordrainDamage] = new StatBuffInfo(swordrainDamage, 0, 0);
        buffPercents[EquipStatType.DamageRange] = new StatBuffInfo(0, damageRange, 0, true);
        buffPercents[EquipStatType.CritRate] = new StatBuffInfo(0, critRate, 0, true);
        buffPercents[EquipStatType.CritDamage] = new StatBuffInfo(0, critDamage, 0, true);
    }

    public void HPIns(int amount)
    {
        currentHP += amount;

        if (currentHP > maxHP) currentHP = maxHP;
    }

    public void HPDes(int amount)
    {
        currentHP -= amount;

        if (currentHP < 0) currentHP = 0;
    }

    public void VHPIns(int amount)
    {
        vHP += amount;

        if (vHP > maxHP) vHP = maxHP;
    }

    public void VHPDes(int amount)
    {
        vHP -= amount;

        if (VHP < 0) vHP = 0;
    }

    public void ManaIns(int percent)
    {
        mana += percent;

        if (mana > 100) mana = 100;
    }

    public void ManaDes(int percent)
    {
        mana -= percent;

        if (mana < 0) mana = 0;
    }

    public void SheildStack(int count)
    {
        shieldCount += count;

        while (shieldCount >= 2)
        {
            shieldStack++;
            shieldCount -= 2;
        }
    }

    public int DamageCalculate(int rawDamage, IEntityBattleStats opStats)
    {
        int damage = rawDamage;
        int minDamage, maxDamage;

        if(damageRange <= 0)
        {
            minDamage = (int)(damage * (100 + damageRange) / 100);
            maxDamage = damage + 1;
        }
        else
        {
            minDamage = damage;
            maxDamage = (int)(damage * (100 + damageRange) / 100) + 1;
        }

        if(CheckCrit())
        {
            Debug.Log("Crit!!!!!");
            minDamage += (int) (minDamage * critDamage / 100);
            maxDamage += (int) (maxDamage * critDamage / 100);
        }

        int defense = opStats.Defense - accuracy >= 0 ? opStats.Defense - accuracy : (int)((opStats.Defense - accuracy) / 2);
        int minFinalDamage = minDamage - defense;
        int maxFinalDamage = maxDamage - defense;
        int finalDamage = Random.Range(minFinalDamage, maxFinalDamage);

        finalDamage = finalDamage < 1 ? 1 : finalDamage;

        Debug.Log("" + finalDamage);

        return finalDamage;
    }

    public bool CheckCrit()
    {
        return Random.value < critRate / 100;
    }

    public void DealDamage(int rawDamage, IEntityBattleStats receiver)
    {
        int damage = DamageCalculate(rawDamage, receiver);
        int receiverVHP = receiver.VHP;
        int lostHP;

        if(damage > receiverVHP)
        {
            lostHP = damage - receiverVHP;
        }
        else
        {
            lostHP = 0;
        }

        receiver.VHPDes(damage);
        receiver.HPDes(lostHP);
    }
}