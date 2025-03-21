using System;
using UnityEngine;

[Serializable]
public class PlayerInfo : EntityInfo
{
    public int CurrentExp;
    public CurrentSkillNode QSkill;
    public CurrentSkillNode ESkill;
    public CurrentSkillNode SpaceSkill;
}