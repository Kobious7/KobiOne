using UnityEngine;

public class SkillButtonAttribute : PropertyAttribute
{
    public SkillButton RequiredSkillButton { get; }

    public SkillButtonAttribute(SkillButton requiredSkillButton)
    {
        RequiredSkillButton = requiredSkillButton;
    }
}