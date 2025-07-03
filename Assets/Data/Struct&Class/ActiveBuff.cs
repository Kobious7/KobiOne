using System;
using UnityEngine;

[Serializable]
public class ActiveBuff
{
    public Sprite Icon;
    public EquipStatType StatBuff;
    public EquipStatType SourceStat;
    public DamageType DamageType;
    public float BuffPercent;
    public float PercentBonus;
    public DurationType DurationType;
    public int Duration;
    public bool DurationStack;
    public bool PercentStack;
    public string Description;
}