using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Battle
{
    public class QSkillBtnUI : SkillBtnUI
    {
        protected override void Click()
        {
            base.Click();
            if(SkillB.Instance.QSkill.skillSO is TileSkillSO) StartCoroutine(SkillB.Instance.SkillActivator.TileSkillActive());
            // if(SkillB.Instance.QSkill is SelfSkillSO) SelfSkillActive();
            // if(SkillB.Instance.QSkill is OpSkillSO) OpSkillActive();
        }

        // private void SelfSkillActive()
        // {
        //     SelfSkillSO selfSkill = SkillB.Instance.QSkill as SelfSkillSO;

        //     Game.Instance.Player.Stats.ManaDes(selfSkill.ManaCost);

        //     //BuffHandling(selfSkill.Buffs);

        //     //if(selfSkill.AnotherTargets == SkillTarget.OPPONENT) DebuffHandling(selfSkill.Debuffs);
        // }

        // private void OpSkillActive()
        // {
        //     OpSkillSO opSkill = SkillB.Instance.QSkill as OpSkillSO;

        //     Game.Instance.Player.Stats.ManaDes(opSkill.ManaCost);

        //     Transform toOpObj = destructiveObjectSpawner.Spawn(destructiveObjectSpawner.GetPrefabsByName("Q"), Game.Instance.Player.transform.position, Quaternion.identity);
        //     toOpObj.GetComponent<DestructiveObject>().Target = Game.Instance.Bot.transform;

        //     toOpObj.gameObject.SetActive(true);

        //     //DebuffHandling(opSkill.Debuffs);

        //     // if(opSkill.AnotherTargets == SkillTarget.SELF) BuffHandling(opSkill.Buffs);
        // }

        protected override bool GetManaCost()
        {
            return SkillB.Instance.QUnlocking;
        }

        private void DebuffHandling(List<Debuff> debuffs)
        {
            if(debuffs.Count <= 0) return;

            for(int i = 0; i < debuffs.Count; i++)
            {
                Transform debuff = DebuffSpawner.Instance.Spawn(DebuffSpawner.Instance.Prefabs[0], Vector3.zero, Quaternion.identity);
                DebuffObject debuffObject = debuff.GetComponent<DebuffObject>();
                debuffObject.Stats = Game.Instance.Bot.Stats;
                debuffObject.DurationType = debuffs[i].durationType;
                debuffObject.Duration = debuffs[i].Duration;
                debuffObject.Stat = debuffs[i].Stat;
                debuffObject.Amount = debuffs[i].Amount;
                debuffObject.Percent = debuffs[i].Percent;

                debuff.gameObject.SetActive(true);
            }
        }

        private void BuffHandling(List<Buff> buffs)
        {
            if(buffs.Count <= 0) return;

            for(int i = 0; i < buffs.Count; i++)
            {
                Transform buff = BuffSpawner.Instance.Spawn(BuffSpawner.Instance.Prefabs[0], Vector3.zero, Quaternion.identity);
                BuffObject buffObject = buff.GetComponent<BuffObject>();
                buffObject.Stats = Game.Instance.Player.Stats;
                buffObject.DurationType = buffs[i].durationType;
                buffObject.Duration = buffs[i].Duration;
                buffObject.Stat = buffs[i].Stat;
                buffObject.Amount = buffs[i].Amount;
                buffObject.Percent = buffs[i].Percent;

                buff.gameObject.SetActive(true);
            }
        }
    }
}