using UnityEngine;
public class BSkillUnlocking : BSkillAb
{
    private BEntityStats playerStats;

    protected override void Start()
    {
        base.Start();

        playerStats = BattleManager.Instance.Player.Stats;
    }

    private void FixedUpdate()
    {
        CheckQUnlocking();
        CheckEUnlocking();
        CheckSpaceUnlocking();
    }

    private void CheckQUnlocking()
    {
        if(BSkill.QSkill != null && BSkill.QSkill.Level > 0)
        {
            ActiveSkillSO skill = (ActiveSkillSO)BSkill.QSkill.SkillSO;

            if(playerStats.Mana >= skill.ManaCost) BSkill.QUnlocking = true;
            else BSkill.QUnlocking = false;
        }
    }

    private void CheckEUnlocking()
    {
        if(BSkill.ESkill != null && BSkill.ESkill.Level > 0)
        {
            ActiveSkillSO skill = (ActiveSkillSO)BSkill.ESkill.SkillSO;

            if(playerStats.Mana >= skill.ManaCost) BSkill.EUnlocking = true;
            else BSkill.EUnlocking = false;
        }
    }

    private void CheckSpaceUnlocking()
    {
        if(BSkill.SpaceSkill != null && BSkill.SpaceSkill.Level > 0)
        {
            ActiveSkillSO skill = (ActiveSkillSO)BSkill.SpaceSkill.SkillSO;

            if(playerStats.Mana >= skill.ManaCost) BSkill.SpaceUnlocking = true;
            else BSkill.SpaceUnlocking = false;
        }
    }
}