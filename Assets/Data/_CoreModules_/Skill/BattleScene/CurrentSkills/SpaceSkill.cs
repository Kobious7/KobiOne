using UnityEngine;

public class SpaceSkill : CurrentSkill
{
    protected override SkillSO GetSkillSO()
    {
        return SkillB.SpaceSkill.skillSO;
    }
}