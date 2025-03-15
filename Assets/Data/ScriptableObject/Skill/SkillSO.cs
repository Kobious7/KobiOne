using UnityEngine;

public class SkillSO : ScriptableObject
{
    public SkillTier SkillTier;
    public SkillBranch SkillBranch;
    public SkillSO PreviousSkill;
    public int PreviousSkillLevelRequirement;
    public int CharacterLevelRequirement;
    public int MaxLevel;
    public int SkillPointCost;
    public string Description;
    public string LockDescription;
    public Sprite SkillIcon;
    public string SkillName;
}