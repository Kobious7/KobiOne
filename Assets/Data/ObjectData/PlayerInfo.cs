using System;
using UnityEngine;

[Serializable]
public class PlayerInfo : EntityInfo
{
    public int CurrentExp;
    public int RequiredExp;
    public int ExpFromBattle;
    public CurrentSkillNode QSkill;
    public CurrentSkillNode ESkill;
    public CurrentSkillNode SpaceSkill;
    public InventoryEquip Weapon;
}