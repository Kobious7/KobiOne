using System.Collections.Generic;
using UnityEngine;

public class AttackTreeTextFormatter : SkillTreeTextFormatter
{
    protected override void CreateTileSkillReplacements(TileSkillSO skill, int level)
    {
        replacements["Area"] = skill.AreaString;
        replacements["ObjectSpawnCount"] = skill.ObjectSpawnCount;
        replacements["SlashDamage"] = $"{(int)(skill.Damage.Scaling + skill.Damage.BonusPerLevel * (level - 1))}";
        if(skill.Debuffs.Count == 1)
        {
            replacements["DefenseDebuffPercent"] = $"{(int)(skill.Debuffs[0].DebuffPercent + skill.Debuffs[0].PercentBonus * (level - 1))}";
        }
    }

    protected override void CreateSelfSkillReplacements(SelfSkillSO skill, int level)
    {
        if(skill.Buffs.Count == 1)
        {
            replacements["AttackBuffPercent"] = $"{(int)(skill.Buffs[0].BuffPercent + skill.Buffs[0].PercentBonus * (level - 1))}";
            replacements["CritDamageBuffPercent"] = $"{(int)(skill.Buffs[0].BuffPercent + skill.Buffs[0].PercentBonus * (level - 1))}"; ;
        }
    }

    protected override void CreateOpponentSkillReplacements(OpSkillSO skill, int level)
    {
        replacements["SlashDamage"] = $"{(int)(skill.Damage.Scaling + skill.Damage.BonusPerLevel * (level - 1))}";
        if(skill.Buffs.Count == 1)
        {
            replacements["MaxHPBuffPercent"] = $"{(int)(skill.Buffs[0].BuffPercent + skill.Buffs[0].PercentBonus * (level - 1))}";
        }
        }

    protected override void CreatePassiveSkillReplacements(PassiveSkillSO skill, int level)
    {
        if(skill.Buffs.Count == 1)
        {
            replacements["PowerFlatValue"] =  $"{(skill.Buffs[0].FlatValue + skill.Buffs[0].FlatBonus * (level - 1))}";
            replacements["PowerPercentValue"] =  $"{(int)(skill.Buffs[0].PercentValue + skill.Buffs[0].PercentBonus * (level - 1))}";
            replacements["AttackPercentValue"] =  $"{(int)(skill.Buffs[0].PercentValue + skill.Buffs[0].PercentBonus * (level - 1))}";
        }
    }
}