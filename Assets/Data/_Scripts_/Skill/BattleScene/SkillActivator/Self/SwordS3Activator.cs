using System.Collections;
using UnityEngine;

public class SwordS3Activator : SkillActivator
{
    public override IEnumerator Activate(SkillNode skill, SkillButton button, BSkillActivator activatorManager)
    {
        activatorManager.IsCasting = true;
        SelfSkillSO selfSkill = (SelfSkillSO)skill.skillSO;

        playerStats.ManaDes(selfSkill.ManaCost);

        ApplyBuff(skill, playerStats, this);

        StartCoroutine(playerAnim.SwordS3SCast());

        if (selfSkill.TurnCount)
        {
            Battle.Instance.TurnCount--;
            Battle.Instance.TurnChange();
        }

        yield return null;
        activatorManager.IsCasting = false;
    }

    public override void EndSkillEffect()
    {
        base.EndSkillEffect();

        playerAnim.EndSwordS3SFX();
    }
}