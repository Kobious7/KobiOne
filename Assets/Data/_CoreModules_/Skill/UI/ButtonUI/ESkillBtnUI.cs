using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESkillBtnUI : SkillBtnUI
{
    protected override SkillNode GetSkill()
    {
        return BSkill.Instance.ESkill;
    }

    protected override void Click()
    {
        base.Click();
        BSkill.Instance.SkillActivator.ActivateSkill(BSkill.Instance.ESkill, SkillButton.E);
    }

    protected override bool GetManaCost()
    {
        return BSkill.Instance.EUnlocking;
    }
}