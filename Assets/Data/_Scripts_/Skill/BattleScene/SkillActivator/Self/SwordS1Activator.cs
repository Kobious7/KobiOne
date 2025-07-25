using System.Collections;
using UnityEngine;

public class SwordS1Activator : SkillActivator
{
    private SelfSkillSO current;
    public override IEnumerator Activate(SkillNode skill, SkillButton button, BSkillActivator activatorManager)
    {
        activatorManager.IsCasting = true;
        SelfSkillSO selfSkill = (SelfSkillSO)skill.SkillSO;
        current = selfSkill;

        playerStats.ManaDes(selfSkill.ManaCost);

        ApplyBuff(skill, playerStats, this);

        StartCoroutine(playerAnim.SwordS1SCast());

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

        Transform fx = SkillObjectFXExtar.Instance.Spawn(current.ExtraFX, current.ExtraFX.position, Quaternion.identity);

        monsterAnim.BeingHit();
        fx.gameObject.SetActive(true);
        Debug.Log("End");
        playerAnim.EndSwordS1SFX();
    }
}