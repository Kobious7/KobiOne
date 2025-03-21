using System;

[Serializable]
public class ActiveDebuff
{
    public EquipStatType StatDebuff;
    public EquipStatType SourceStat;
    public float DebuffPercent;
    public float PercentBonus;
    public DurationType DurationType;
    public int Duration;
    public bool DurationStack;
    public bool PercentStack;
}