using System;

[Serializable]
public class Stat
{
    public int Index;
    public string Name;
    public int TrueValue;
    public int Value;
    public bool IsPercentValue;
    public int FlatBonus;
    public float PercentBonus;

    public Stat(int index, string name)
    {
        Index = index;
        Name = name;
    }

    public Stat(Stat stat)
    {
        Index = stat.Index;
        Name = stat.Name;
        TrueValue = stat.TrueValue;
        Value = stat.Value;
        IsPercentValue = stat.IsPercentValue;
        FlatBonus = stat.FlatBonus;
        PercentBonus = stat.PercentBonus;
    }
}