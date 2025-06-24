using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QSkillBtnUI : SkillBtnUI
{
    protected override SkillNode GetSkill()
    {
        return BSkill.Instance.QSkill;
    }

    protected override void Click()
    {
        base.Click();
        BSkill.Instance.SkillActivator.ActivateSkill(BSkill.Instance.QSkill, SkillButton.Q);
    }

    protected override bool GetManaCost()
    {
        return BSkill.Instance.QUnlocking;
    }
}