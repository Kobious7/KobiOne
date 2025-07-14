using System.Collections;
using UnityEngine;

public class SwordA1Activator : SkillActivator
{
    private int rawDamage;

    public override IEnumerator Activate(SkillNode skill, SkillButton button, BSkillActivator activatorManager)
    {
        activatorManager.IsCasting = true;
        OpSkillSO opSkill = (OpSkillSO)skill.skillSO;

        playerStats.ManaDes(opSkill.ManaCost);

        rawDamage = bSkill.Calculator.SkillDamageCalculate(playerStats, skill);

        playerAnim.SwordA1SCast1();
        yield return StartCoroutine(playerAnim.WaitAnim("SwordA1SCast1"));
        yield return StartCoroutine(playerMovement.SwordA1S_DashForward());
        playerAnim.SwordA1SCast2();
        yield return StartCoroutine(playerAnim.WaitAnim("SwordA1SCast2"));
        yield return StartCoroutine(playerMovement.SwordA1S_MoveBack());

        if (opSkill.Self && opSkill.Buffs.Count > 0)
        {
            ApplyBuff(skill, playerStats, this);
        }

        PlayerVFXManager.Instance.PlayeSwordA1SHealingVFX();

        if (opSkill.TurnCount)
        {
            Battle.Instance.TurnCount--;
            Battle.Instance.TurnChange();
        }
        
        activatorManager.IsCasting = false;
    }

    public override void DealOpSkillDamage()
    {
        playerStats.DealDamage(rawDamage, opStats, DamageType.SlashDamage);
        monsterAnim.BeingHit();
    }
}