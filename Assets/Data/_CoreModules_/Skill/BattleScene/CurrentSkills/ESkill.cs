using UnityEngine;

public class ESkill : CurrentSkill
{
    protected override SkillSO GetSkillSO()
    {
        return SkillB.ESkill.skillSO;
    }
}