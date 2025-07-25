using System;

[Serializable]
public class CurrentSkillData
{
    public int Level;
    public int TreeIndex;

    public CurrentSkillData(int level, int treeIndex)
    {
        Level = level;
        TreeIndex = treeIndex;
    }
}