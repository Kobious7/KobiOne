using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class SkillLevelList
{
    public List<int> SkillLevel;

    public SkillLevelList(SkillLevelList list)
    {
        List<int> newList = new();

        foreach (var item in list.SkillLevel)
        {
            newList.Add(item);
        }

        SkillLevel = newList;
    }
}