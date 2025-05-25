using UnityEngine;
public class SkillBUnlocking : SkillBAb
{
    private IEntityBattleStats playerStats;

    protected override void Start()
    {
        base.Start();

        playerStats = Game.Instance.Player.BattleStats;
    }

    private void FixedUpdate()
    {
        CheckQUnlocking();
        CheckEUnlocking();
        CheckSpaceUnlocking();
    }

    private void CheckQUnlocking()
    {
        if(SkillB.QSkill != null && SkillB.QSkill.Level > 0)
        {
            ActiveSkillSO skill = (ActiveSkillSO)SkillB.QSkill.skillSO;

            if(playerStats.Mana >= skill.ManaCost) SkillB.QUnlocking = true;
            else SkillB.QUnlocking = false;
        }
    }

    private void CheckEUnlocking()
    {
        if(SkillB.ESkill != null && SkillB.ESkill.Level > 0)
        {
            ActiveSkillSO skill = (ActiveSkillSO)SkillB.ESkill.skillSO;

            if(playerStats.Mana >= skill.ManaCost) SkillB.EUnlocking = true;
            else SkillB.EUnlocking = false;
        }
    }

    private void CheckSpaceUnlocking()
    {
        if(SkillB.SpaceSkill != null && SkillB.SpaceSkill.Level > 0)
        {
            ActiveSkillSO skill = (ActiveSkillSO)SkillB.SpaceSkill.skillSO;

            if(playerStats.Mana >= skill.ManaCost) SkillB.SpaceUnlocking = true;
            else SkillB.SpaceUnlocking = false;
        }
    }
}