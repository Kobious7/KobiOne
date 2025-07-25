using UnityEngine;

public class SkillPointResetUI : ResetPromptUI
{
    protected override void ClickToResetListener()
    {
        InfiniteMapManager.Instance.Skill.ResetAllSkill();
        this.transform.gameObject.SetActive(false);
    }
}