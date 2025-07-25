using System;
using UnityEngine;

[Serializable]
public class MonsterInfo : EntityInfo
{
    public string Name;
    public int ZeroLevel;
    public AttackType AttackType;
    public MonsterTier Tier;
}