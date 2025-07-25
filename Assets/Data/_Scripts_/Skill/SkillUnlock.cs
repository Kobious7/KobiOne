using UnityEngine;

public class SkillUnlock : SkillAb
{
    public void CheckSkillUnlock(int level)
    {
        for (int i = 0; i < Skill.SkillTreeList.Count; i++)
        {
            for (int j = 0; j < Skill.SkillTreeList[i].SkillList.Count; j++)
            {
                if (Skill.SkillTreeList[i].SkillList[j].Level > 0) continue;
                if (level < Skill.SkillTreeList[i].SkillList[j].SkillSO.CharacterLevelRequirement) continue;

                Skill.SkillTreeList[i].SkillList[j].Level = 1;
                Skill.SkillTreeList[i].SkillList[j].IsUnlocked = true;

                //Save Data
                InfiniteMapManager.Instance.PlayerData.SkillTreeLevels[i].SkillLevel[j] = Skill.SkillTreeList[i].SkillList[j].Level;
            }
        }
    }
}