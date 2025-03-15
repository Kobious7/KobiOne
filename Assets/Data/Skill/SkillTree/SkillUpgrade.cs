using UnityEngine;

public class SkillUpgrade : SkillAb
{
    public void Upgrade(SkillNode skill, int treeIndex)
    {
        bool canUpgrade = Skill.CheckSkillPoints(skill.skillSO.SkillPointCost);

        if(!canUpgrade) return;

        skill.Level++;

        Skill.DecreaseSkillPoints(skill.skillSO.SkillPointCost);

        if(skill.skillSO is PassiveSkillSO)
        {
            Skill.BonusUpdating.UpdateBonus(skill, treeIndex);
        }
    }
}