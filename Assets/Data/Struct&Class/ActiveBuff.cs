using System;

[Serializable]
public class ActiveBuff
{
    public EquipStatType StatBuff;
    public EquipStatType SourceStat;
    public float BuffPercent;
    public float PercentBonus;
    public DurationType DurationType;
    public int Duration;
}