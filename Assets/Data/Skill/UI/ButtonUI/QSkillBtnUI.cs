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
    }
}