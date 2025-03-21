using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    public bool IsNew;
    public int Level;
    public int CurrentExp;
    public int RemainPoints;
    public Stat Power;
    public Stat Magic;
    public Stat Strength;
    public Stat Defense;
    public Stat Dexterity;
    public int SkillPoints;
    public CurrentSkillData QSkill;
    public CurrentSkillData ESkill;
    public CurrentSkillData SpaceSkill;
    public List<SkillLevelData> SkillTreeLevels;
}