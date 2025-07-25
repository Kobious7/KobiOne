using System;
using UnityEngine;

public class SkillUpgrade : SkillAb
{
    public void Upgrade(SkillNode skill, int treeIndex)
    {
        bool canUpgrade = Skill.CheckSkillPoints(skill.SkillSO.SkillPointCost);

        if (!canUpgrade) return;

        skill.Level++;

        Skill.DecreaseSkillPoints(skill.SkillSO.SkillPointCost);

        if (skill.SkillSO is PassiveSkillSO)
        {
            Skill.BonusUpdating.UpdateBonusAfterUpgrading(skill, treeIndex);
        }

        //Save Data
        InfiniteMapManager.Instance.PlayerData.SkillTreeLevels[treeIndex].SkillLevel[skill.Index] = skill.Level;
    }
}