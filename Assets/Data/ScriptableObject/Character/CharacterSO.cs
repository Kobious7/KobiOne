using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjects/Character")]
public class CharacterSO : ScriptableObject
{
    public bool IsLoaded;
    public bool IsNew;
    public int Level;
    public int CurrentExp;

    [Header("Potential")]
    public int RemainPoints;
    public Stat Power;
    public Stat Magic;
    public Stat Strength;
    public Stat Defense;
    public Stat Dexterity;

    [Header("Skill")]
    public int SkillPoints;
    public CurrentSkillNode QSkill;
    public CurrentSkillNode ESkill;
    public CurrentSkillNode SpaceSkill;
    public List<SkillLevelData> SkillTreeLevels;
}