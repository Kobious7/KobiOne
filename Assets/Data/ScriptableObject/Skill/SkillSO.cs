using UnityEngine;

public class SkillSO : ScriptableObject
{
    public SkillBranch SkillBranch;
    public SkillTier SkillTier;
    public int CharacterLevelRequirement;
    public int MaxLevel;
    public int SkillPointCost;
    public string Description;
    public string LockDescription;
    public Sprite SkillIcon;
    public string SkillName;
}