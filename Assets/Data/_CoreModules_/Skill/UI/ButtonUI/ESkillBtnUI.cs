using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESkillBtnUI : SkillBtnUI
{
    protected override SkillNode GetSkill()
    {
        return SkillB.Instance.ESkill;
    }

    protected override void Click()
    {
        base.Click();
        SkillB.Instance.SkillActivator.Active(SkillB.Instance.ESkill, SkillButton.E);
    }

    protected override bool GetManaCost()
    {
        return SkillB.Instance.EUnlocking;
    }
}