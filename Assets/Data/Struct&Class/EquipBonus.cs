using System;

[Serializable]
public class EquipBonus
{
    public EquipStatType Stat;
    public int FlatValue;
    public float PercentValue;

    public EquipBonus() {}

    public EquipBonus(EquipStatType stat)
    {
        Stat = stat;
    }
}