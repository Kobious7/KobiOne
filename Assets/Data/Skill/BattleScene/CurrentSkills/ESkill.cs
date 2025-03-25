using UnityEngine;

namespace Battle
{
    public class ESkill : CurrentSkill
    {
        protected override SkillSO GetSkillSO()
        {
            return SkillB.ESkill.skillSO;
        }
    }
}