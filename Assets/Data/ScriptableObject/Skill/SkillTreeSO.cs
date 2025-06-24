using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillTreeSO", menuName = "ScriptableObjects/SkillTree")]
public class SkillTreeSO : ScriptableObject
{
    public string Name;
    public int Index;
    public SkillNode Root;
    public SkillNode Attack1;
    public SkillNode Attack2;
    public SkillNode Attack3;
    public SkillNode Support1;
    public SkillNode Support2;
    public SkillNode Support3;
}