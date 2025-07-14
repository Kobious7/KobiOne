using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceSkillBtnUI : SkillBtnUI
{
    protected override SkillNode GetSkill()
    {
        return BSkill.Instance.SpaceSkill;
    }

    protected override void Click()
    {
        base.Click();
        BSkill.Instance.SkillActivator.ActivateSkill(BSkill.Instance.SpaceSkill, SkillButton.Space);
    }

    protected override bool GetManaCost()
    {
        return BSkill.Instance.SpaceUnlocking;
    }
}