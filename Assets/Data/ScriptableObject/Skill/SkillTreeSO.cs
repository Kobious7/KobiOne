using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillTreeSO", menuName = "ScriptableObjects/SkillTree")]
public class SkillTreeSO : ScriptableObject
{
    public string Name;
    public int Index;
    public SkillNode T1B0;
    public SkillNode T2B1;
    public SkillNode T2B2;
    public SkillNode T2B3;
    public SkillNode T3B1;
    public SkillNode T3B2;
    public SkillNode T3B3;
}