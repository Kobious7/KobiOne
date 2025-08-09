using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryEquip : InventoryStuff
{
    [SerializeField] private int level;
    [SerializeField] private EquipSO equipSO;
    [SerializeField] private Rarity rarity;
    [SerializeField] private EquipStat mainStat;
    [SerializeField] private List<EquipStat> subStats;
    [SerializeField] private int currentUpgradeCost;
    [SerializeField] private int nextUpgradeCost;
    [SerializeField] private bool isNew;
    [SerializeField] private bool isLock;

    #region Properties
    public int Level
    {
        get => level;
        set => level = value;
    }

    public EquipSO EquipSO
    {
        get => equipSO;
        set => equipSO = value;
    }

    public Rarity Rarity
    {
        get => rarity;
        set => rarity = value;
    }

    public EquipStat MainStat
    {
        get => mainStat;
        set => mainStat = value;
    }

    public List<EquipStat> SubStats
    {
        get => subStats;
        set => subStats = value;
    }

    public int CurrentUpgradeCost
    {
        get => currentUpgradeCost;
        set => currentUpgradeCost = value;
    }

    public int NextUpgradeCost
    {
        get => nextUpgradeCost;
        set => nextUpgradeCost = value;
    }

    public bool IsNew { get => isNew; set => isNew = value; }
    public bool IsLock { get => isLock; set => isLock = value; }
    #endregion

    public InventoryEquip()
    {

    }

    public InventoryEquip(EquipSO equipSO)
    {
        this.equipSO = equipSO;
    }

    public InventoryEquip(InventoryEquip equip)
    {
        level = equip.Level;
        equipSO = equip.EquipSO;
        rarity = equip.Rarity;
        mainStat = equip.MainStat;
        subStats = new(equip.subStats);
        currentUpgradeCost = equip.CurrentUpgradeCost;
        nextUpgradeCost = equip.NextUpgradeCost;
        isNew = equip.IsNew;
        isLock = equip.IsLock;
    }
}