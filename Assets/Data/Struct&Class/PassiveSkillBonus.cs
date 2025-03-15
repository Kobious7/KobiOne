using System;

[Serializable]
public class PassiveSkillBonus
{
    public EquipStatType Stat;
    public int FlatValue;
    public float PercentValue;

    public PassiveSkillBonus() {}

    public PassiveSkillBonus(EquipStatType stat)
    {
        Stat = stat;
    }
}