using System;

[Serializable]
public struct Buff
{
    public string Name;
    public StatType Stat;
    public int Amount;
    public int Percent;
    public DurationType durationType;
    public int Duration;
    public bool DurationStack;
}