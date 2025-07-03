using System;
using UnityEngine;

[Serializable]
public class ActiveDebuff
{
    public Sprite Icon;
    public EquipStatType StatDebuff;
    public EquipStatType SourceStat;
    public float DebuffPercent;
    public float PercentBonus;
    public DurationType DurationType;
    public int Duration;
    public bool DurationStack;
    public bool PercentStack;
    public string Description;
}