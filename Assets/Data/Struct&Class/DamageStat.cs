using System;

[Serializable]
public class DamageStat
{
    public EquipStatType SourceDamage;
    public DamageType damageType;
    public float Scaling;
    public float BonusPerLevel;
}