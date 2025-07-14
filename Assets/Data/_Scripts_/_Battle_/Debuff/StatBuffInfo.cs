using System;

[Serializable]
public class StatBuffInfo
{
    public int OriginFlatValue;
    public float OriginPercentValue;
    public bool IsPercentValue;
    public float PercentBonus;

    public StatBuffInfo() {}

    public StatBuffInfo(int originFlatValue, float originPercentValue, float percentBonus, bool isPercentValue = false)
    {
        OriginFlatValue = originFlatValue;
        OriginPercentValue = originPercentValue;
        PercentBonus = percentBonus;
        IsPercentValue = isPercentValue;
    }
}