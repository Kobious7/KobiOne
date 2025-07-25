using System;
using System.Collections.Generic;

[Serializable]
public class SkillTree : GMono
{
    public int Index;
    public bool IsActive;
    public List<WeaponType> TypeRequires;
    public List<SkillNode> SkillList;
}