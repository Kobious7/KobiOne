using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceSkillBtnUI : SkillBtnUI
{
    protected override SkillNode GetSkill()
    {
        return SkillB.Instance.SpaceSkill;
    }

    protected override void Click()
    {
        base.Click();
        SkillB.Instance.SkillActivator.Active(SkillB.Instance.SpaceSkill, SkillButton.Space);
    }

    protected override bool GetManaCost()
    {
        return SkillB.Instance.SpaceUnlocking;
    }
}