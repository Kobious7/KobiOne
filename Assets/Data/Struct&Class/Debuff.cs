using System;

[Serializable]
public struct Debuff
{
    public string Name;
    public Stat Stat;
    public int Amount;
    public int Percent;
    public DurationType durationType;
    public int Duration;
    public bool DurationStack;
}