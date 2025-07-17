using System;

[Serializable]
public class OtherSourcesBonus
{
    public EquipStatType Stat;
    public int FlatValue;
    public float PercentValue;

    public OtherSourcesBonus() {}

    public OtherSourcesBonus(EquipStatType stat)
    {
        Stat = stat;
    }
}