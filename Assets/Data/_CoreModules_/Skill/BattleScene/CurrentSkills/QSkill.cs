using UnityEngine;

public class QSkill : CurrentSkill
{
    protected override SkillSO GetSkillSO()
    {
        return SkillB.QSkill.skillSO;
    }
}