using UnityEngine;

public class QSkill : CurrentSkill
{
    protected override SkillSO GetSkillSO()
    {
        return BSkill.QSkill.SkillSO;
    }
}