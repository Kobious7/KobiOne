using System;

[Serializable]
public class CurrentSkillData
{
    public int Level;
    public int TreeIndex;
    public string NodeName;

    public CurrentSkillData(int level, int treeIndex, string nodeName)
    {
        Level = level;
        TreeIndex = treeIndex;
        NodeName = nodeName;
    }
}