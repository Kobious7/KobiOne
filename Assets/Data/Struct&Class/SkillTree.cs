using System;
using System.Collections.Generic;

[Serializable]
public class SkillTree : GMono
{
    public int Index;
    public List<WeaponType> TypeRequires;
    public bool IsActive;
    public SkillNode Root;
    public SkillNode Attack1;
    public SkillNode Attack2;
    public SkillNode Attack3;
    public SkillNode Support1;
    public SkillNode Support2;
    public SkillNode Support3;

    public SkillNode GetSkillNodeByName(string nodeName)
    {
        if(nodeName == "Root") return Root;
        if(nodeName == "Attack1") return Attack1;
        if(nodeName == "Attack2") return Attack2;
        if(nodeName == "Attack3") return Attack3;
        if(nodeName == "Support1") return Support1;
        if(nodeName == "Support2") return Support2;
        if(nodeName == "Support3") return Support3;
        return null;
    }
}