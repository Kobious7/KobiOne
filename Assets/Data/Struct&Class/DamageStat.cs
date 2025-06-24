using System;

[Serializable]
public class DamageStat
{
    public EquipStatType SourceDamage;
    public DamageType DamageType;
    public float Scaling;
    public float BonusPerLevel;
}