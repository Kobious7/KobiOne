using System.Collections.Generic;
using UnityEngine;

public class Equipment : GMono
{
    [SerializeField] private InventoryEquip weapon;
    [SerializeField] private InventoryEquip helmet;
    [SerializeField] private InventoryEquip bodyArmor;
    [SerializeField] private InventoryEquip legArmor;
    [SerializeField] private InventoryEquip boots;
    [SerializeField] private InventoryEquip backItem;
    [SerializeField] private InventoryEquip aura;
    [SerializeField] private List<EquipBonus> statsBonus;
    [SerializeField] private EquipmentCalculation calculator;

    public EquipmentCalculation Calculator => calculator;

    [SerializeField] private EquipDisarming unequip;

    public EquipDisarming Unequip => unequip;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        CreateStatsBonus();
        LoadEquipmentCalculation();
        calculator.CalculateBonus();
        LoadUnequip();
    }

    private void CreateStatsBonus()
    {
        weapon = new InventoryEquip();
        weapon.SubStats = new List<EquipStat>();
        helmet = new InventoryEquip();
        helmet.SubStats = new List<EquipStat>();
        bodyArmor = new InventoryEquip();
        bodyArmor.SubStats = new List<EquipStat>();
        legArmor = new InventoryEquip();
        legArmor.SubStats = new List<EquipStat>();
        boots = new InventoryEquip();
        boots.SubStats = new List<EquipStat>();
        backItem = new InventoryEquip();
        backItem.SubStats = new List<EquipStat>();
        aura = new InventoryEquip();
        aura.SubStats = new List<EquipStat>();
        
        NewStatBonus();
    }

    private void LoadEquipmentCalculation()
    {
        if(calculator != null) return;

        calculator = GetComponentInChildren<EquipmentCalculation>();
    }

    private void LoadUnequip()
    {
        if(unequip != null) return;

        unequip = GetComponentInChildren<EquipDisarming>();
    }

    public EquipBonus GetEquipBonusByStat(EquipStatType statType)
    {
        foreach(var stat in statsBonus)
        {
            if(stat.Stat == statType) return stat;
        }

        return null;
    }

    public void NewStatBonus()
    {
        statsBonus = new List<EquipBonus>
        {
            new EquipBonus(EquipStatType.Power), new EquipBonus(EquipStatType.Magic), new EquipBonus(EquipStatType.Strength),
            new EquipBonus(EquipStatType.DefenseP), new EquipBonus(EquipStatType.Dexterity), new EquipBonus(EquipStatType.Attack),
            new EquipBonus(EquipStatType.MagicAttack), new EquipBonus(EquipStatType.HP), new EquipBonus(EquipStatType.Defense),
            new EquipBonus(EquipStatType.Accuracy), new EquipBonus(EquipStatType.DamageRange), new EquipBonus(EquipStatType.Speed),
            new EquipBonus(EquipStatType.CritRate), new EquipBonus(EquipStatType.CritDamage)
        };
    }

    public InventoryEquip Weapon
    {
        get => weapon;
        set => weapon = value;
    }

    public InventoryEquip Helmet
    {
        get => helmet;
        set => helmet = value;
    }

    public InventoryEquip BodyArmor
    {
        get => bodyArmor;
        set => bodyArmor = value;
    }

    public InventoryEquip LegArmor
    {
        get => legArmor;
        set => legArmor = value;
    }

    public InventoryEquip Boots
    {
        get => boots;
        set => boots = value;
    }

    public InventoryEquip BackItem
    {
        get => backItem;
        set => backItem = value;
    }

    public InventoryEquip Aura
    {
        get => aura;
        set => aura = value;
    }

    public List<EquipBonus> StatsBonus
    {
        get => statsBonus;
        set => statsBonus = value;
    }
}