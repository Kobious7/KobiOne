using System.Collections.Generic;
using UnityEngine;

public class Equipment : GMono
{
    [SerializeField] private InventoryEquip weapon;
    [SerializeField] private InventoryEquip helmet;
    [SerializeField] private InventoryEquip armor;
    [SerializeField] private InventoryEquip armwear;
    [SerializeField] private InventoryEquip boots;
    [SerializeField] private InventoryEquip special;
    [SerializeField] private List<OtherSourcesBonus> statsBonus;
    [SerializeField] private EquipmentCalculator calculator;

    public EquipmentCalculator Calculator => calculator;

    [SerializeField] private EquipDisarming unequip;

    public EquipDisarming Unequip => unequip;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        CreateStatsBonus();
        LoadEquipmentCalculator();
        LoadUnequip();
    }

    private void CreateStatsBonus()
    {
        weapon = new InventoryEquip();
        weapon.SubStats = new List<EquipStat>();
        helmet = new InventoryEquip();
        helmet.SubStats = new List<EquipStat>();
        armor = new InventoryEquip();
        armor.SubStats = new List<EquipStat>();
        armwear = new InventoryEquip();
        armwear.SubStats = new List<EquipStat>();
        boots = new InventoryEquip();
        boots.SubStats = new List<EquipStat>();
        special = new InventoryEquip();
        special.SubStats = new List<EquipStat>();
        
        NewStatBonus();
    }

    private void LoadEquipmentCalculator()
    {
        if(calculator != null) return;

        calculator = GetComponentInChildren<EquipmentCalculator>();
    }

    private void LoadUnequip()
    {
        if(unequip != null) return;

        unequip = GetComponentInChildren<EquipDisarming>();
    }

    public OtherSourcesBonus GetEquipBonusByStat(EquipStatType statType)
    {
        foreach(var stat in statsBonus)
        {
            if(stat.Stat == statType) return stat;
        }

        return null;
    }

    public void NewStatBonus()
    {
        statsBonus = new List<OtherSourcesBonus>
        {
            new OtherSourcesBonus(EquipStatType.Power), new OtherSourcesBonus(EquipStatType.Magic), new OtherSourcesBonus(EquipStatType.Strength),
            new OtherSourcesBonus(EquipStatType.DefenseP), new OtherSourcesBonus(EquipStatType.Dexterity), new OtherSourcesBonus(EquipStatType.Attack),
            new OtherSourcesBonus(EquipStatType.MagicAttack), new OtherSourcesBonus(EquipStatType.HP), new OtherSourcesBonus(EquipStatType.Defense),
            new OtherSourcesBonus(EquipStatType.Accuracy), new OtherSourcesBonus(EquipStatType.DamageRange), new OtherSourcesBonus(EquipStatType.Speed),
            new OtherSourcesBonus(EquipStatType.CritRate), new OtherSourcesBonus(EquipStatType.CritDamage)
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

    public InventoryEquip Armor
    {
        get => armor;
        set => armor = value;
    }

    public InventoryEquip Armwear
    {
        get => armwear;
        set => armwear = value;
    }

    public InventoryEquip Boots
    {
        get => boots;
        set => boots = value;
    }

    public InventoryEquip Special
    {
        get => special;
        set => special = value;
    }

    public List<OtherSourcesBonus> StatsBonus
    {
        get => statsBonus;
        set => statsBonus = value;
    }
}