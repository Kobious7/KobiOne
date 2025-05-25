using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QSkillBtnUI : SkillBtnUI
{
    protected override SkillNode GetSkill()
    {
        return SkillB.Instance.QSkill;
    }

    protected override void Click()
    {
        base.Click();
        SkillB.Instance.SkillActivator.Active(SkillB.Instance.QSkill, SkillButton.Q);
    }

    protected override bool GetManaCost()
    {
        return SkillB.Instance.QUnlocking;
    }
}