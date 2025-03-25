using System;

[Serializable]
public class ActiveBuff
{
    public EquipStatType StatBuff;
    public EquipStatType SourceStat;
    public DamageType DamageType;
    public float BuffPercent;
    public float PercentBonus;
    public DurationType DurationType;
    public int Duration;
    public bool DurationStack;
    public bool PercentStack;
}