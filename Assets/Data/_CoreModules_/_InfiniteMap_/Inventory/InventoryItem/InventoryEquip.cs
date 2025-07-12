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

    public InventoryEquip()
    {

    }

    public InventoryEquip(EquipSO equipSO)
    {

    }
}