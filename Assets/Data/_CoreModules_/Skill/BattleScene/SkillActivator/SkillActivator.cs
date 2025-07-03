using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillActivator : GMono, ISkillActivate
{
    protected DestructiveObjectSpawner destructiveObjectSpawner;
    protected BattleManager battleManager;
    protected BEntityStats playerStats;
    protected BEntityStats opStats;
    protected BPlayerAnim playerAnim;
    protected BPlayerMovement playerMovement;
    protected BMonsterAnim monsterAnim;
    protected BSkill bSkill;

    protected override void Start()
    {
        base.Start();

        destructiveObjectSpawner = DestructiveObjectSpawner.Instance;
        battleManager = BattleManager.Instance;
        playerStats = battleManager.Player.Stats;
        opStats = battleManager.Monster.Stats;
        playerAnim = battleManager.Player.Anim as BPlayerAnim;
        playerMovement = battleManager.Player.Movement as BPlayerMovement;
        monsterAnim = battleManager.Monster.Anim as BMonsterAnim;
        bSkill = BSkill.Instance;
    }
    public abstract IEnumerator Activate(SkillNode skill, SkillButton button);

    public void ApplyDebuff(SkillNode skill, BEntityStats stats)
    {
        int level = skill.Level;
        List<ActiveDebuff> debuffs = new();

        if (skill.skillSO is TileSkillSO tileSkill)
        {
            debuffs = tileSkill.Debuffs;
        }

        if (skill.skillSO is SelfSkillSO selfSkill)
        {
            debuffs = selfSkill.Debuffs;
        }

        if (skill.skillSO is OpSkillSO opSkill)
        {
            debuffs = opSkill.Debuffs;
        }

        OpponentDebuffSpawner.Instance.SpawnDebuffs(level, debuffs, stats);
    }

    public void ApplyBuff(SkillNode skill, BEntityStats stats, SkillActivator activator)
    {
        int level = skill.Level;
        List<ActiveBuff> buffs = new();

        if (skill.skillSO is TileSkillSO tileSkill)
        {
            buffs = tileSkill.Buffs;
        }

        if (skill.skillSO is SelfSkillSO selfSkill)
        {
            buffs = selfSkill.Buffs;
        }

        if (skill.skillSO is OpSkillSO opSkill)
        {
            buffs = opSkill.Buffs;
        }

        PlayerBuffSpawner.Instance.SpawnBuffs(level, buffs, stats, activator);
    }

    public virtual void DealOpSkillDamage() { }

    public virtual void EndSkillEffect() { }
}