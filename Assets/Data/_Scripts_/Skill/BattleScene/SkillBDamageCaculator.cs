using UnityEngine;

public class BSkillDamageCalculator : BSkillAb
{
    public int SkillDamageCalculate(BEntityStats dealer, SkillNode skill)
    {
        return CalculateDamage(dealer, skill);
    }

    private int CalculateDamage(BEntityStats dealer, SkillNode skill)
    {
        int level = skill.Level;

        if (skill.skillSO is TileSkillSO tileSkill)
        {
            if (tileSkill.Damage.SourceDamage == EquipStatType.Attack)
                return (int)(dealer.Attack * ((level - 1) * tileSkill.Damage.BonusPerLevel + tileSkill.Damage.Scaling) / 100);
            if (tileSkill.Damage.SourceDamage == EquipStatType.MagicAttack)
                return (int)(dealer.MagicAttack * ((level - 1) * tileSkill.Damage.BonusPerLevel + tileSkill.Damage.Scaling) / 100);
            if (tileSkill.Damage.SourceDamage == EquipStatType.SlashDamage)
                return (int)(dealer.SlashDamage * ((level - 1) * tileSkill.Damage.BonusPerLevel + tileSkill.Damage.Scaling) / 100);
        }

        if (skill.skillSO is OpSkillSO opSkill)
        {
            if (opSkill.Damage.SourceDamage == EquipStatType.Attack)
                return (int)(dealer.Attack * ((level - 1) * opSkill.Damage.BonusPerLevel + opSkill.Damage.Scaling) / 100);
            if (opSkill.Damage.SourceDamage == EquipStatType.MagicAttack)
                return (int)(dealer.MagicAttack * ((level - 1) * opSkill.Damage.BonusPerLevel + opSkill.Damage.Scaling) / 100);
            if (opSkill.Damage.SourceDamage == EquipStatType.SlashDamage)
                return (int)(dealer.SlashDamage * ((level - 1) * opSkill.Damage.BonusPerLevel + opSkill.Damage.Scaling) / 100);
        }

        return 0;
    }
}