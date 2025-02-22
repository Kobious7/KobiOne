using System;

[Serializable]
public class EquipStat
{
    public EquipStatType Stat;
    public TypeBonus TypeBonus;
    public int FlatBaseValue;
    public float PercentBaseValue;
    public int FlatValue;
    public float PercentValue;
    public int FlatBonus;
    public float PercentBonus;

    public EquipStat() {}

    public EquipStat(EquipStatType stat)
    {
        Stat = stat;
    }
}