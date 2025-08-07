using System;

[Serializable]
public class CurrentSkillNode : SkillNode
{
    public int TreeIndex;

    public CurrentSkillNode() {}

    public CurrentSkillNode(CurrentSkillNode skill)
    {
        TreeIndex = skill.TreeIndex;
        Index = skill.Index;
        Level = skill.Level;
        IsUnlocked = skill.IsUnlocked;
        SkillSO = skill.SkillSO;
    }
}