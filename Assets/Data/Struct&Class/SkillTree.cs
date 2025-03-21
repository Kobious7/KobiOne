using System;

[Serializable]
public class SkillTree : GMono
{
    public SkillNode T1B0;
    public SkillNode T2B1;
    public SkillNode T2B2;
    public SkillNode T2B3;
    public SkillNode T3B1;
    public SkillNode T3B2;
    public SkillNode T3B3;

    public SkillNode GetSkillNodeByName(string nodeName)
    {
        if(nodeName == "T1B0") return T1B0;
        if(nodeName == "T2B1") return T2B1;
        if(nodeName == "T2B2") return T2B2;
        if(nodeName == "T2B3") return T2B3;
        if(nodeName == "T3B1") return T3B1;
        if(nodeName == "T3B2") return T3B2;
        if(nodeName == "T3B3") return T3B3;
        return null;
    }
}