using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public abstract class SkillTreeTextFormatter : GMono
{
    protected Dictionary<string, object> replacements;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        replacements = new();
    }

    public string GetReplacedDescription(SkillNode skill, int level)
    {
        if(skill.skillSO is ActiveSkillSO)
        {
            if(skill.skillSO is TileSkillSO)
            {
                TileSkillSO tileSkill = (TileSkillSO)skill.skillSO;

                CreateTileSkillReplacements(tileSkill, level);
            }

            if(skill.skillSO is SelfSkillSO)
            {
                SelfSkillSO selfSkill = (SelfSkillSO)skill.skillSO;

                CreateSelfSkillReplacements(selfSkill, level);
            }

            if(skill.skillSO is OpSkillSO)
            {
                OpSkillSO opSkill = (OpSkillSO)skill.skillSO;

                CreateOpponentSkillReplacements(opSkill, level);
            }
        }
        else
        {
            PassiveSkillSO passiveSkill = (PassiveSkillSO)skill.skillSO;

            CreatePassiveSkillReplacements(passiveSkill, level);
        }

        return ReplaceSkillDescription(skill);
    }

    protected abstract void CreateTileSkillReplacements(TileSkillSO skill, int level);
    protected abstract void CreateSelfSkillReplacements(SelfSkillSO skill, int level);
    protected abstract void CreateOpponentSkillReplacements(OpSkillSO skill, int level);
    protected abstract void CreatePassiveSkillReplacements(PassiveSkillSO skill, int level);

    private string ReplaceSkillDescription(SkillNode skill)
    {
        string replacedDescription = skill.skillSO.Description;

        foreach(var replacement in replacements)
        {
            replacedDescription = replacedDescription.Replace("{" + replacement.Key + "}", replacement.Value.ToString());
        }

        return replacedDescription;
    }
}