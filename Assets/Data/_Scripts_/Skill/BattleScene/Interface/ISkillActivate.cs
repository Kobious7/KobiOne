using System.Collections;

public interface ISkillActivate
{
    IEnumerator Activate(SkillNode skill, SkillButton button, BSkillActivator activatorManager);
}