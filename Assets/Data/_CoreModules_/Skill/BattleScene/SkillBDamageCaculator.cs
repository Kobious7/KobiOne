using UnityEngine;

public class SkillBDamageCalculator : SkillBAb
{
    public int SkillDamageCalculate(IEntityBattleStats dealer, SkillNode skill)
    {
        return CalculateDamage(dealer, skill);
    }

    private int CalculateDamage(IEntityBattleStats dealer, SkillNode skill)
    {
        int level = skill.Level;

        if(skill.skillSO is TileSkillSO tileSkill)
        {
            if(tileSkill.Damage.SourceDamage == EquipStatType.Attack)
                return (int)(dealer.Attack * ((level - 1) * tileSkill.Damage.BonusPerLevel + tileSkill.Damage.Scaling) / 100);
            if(tileSkill.Damage.SourceDamage == EquipStatType.MagicAttack)
                return (int)(dealer.MagicAttack * ((level - 1) * tileSkill.Damage.BonusPerLevel + tileSkill.Damage.Scaling) / 100);
        }

        if(skill.skillSO is OpSkillSO opSkill)
        {
            if(opSkill.Damage.SourceDamage == EquipStatType.Attack)
                return (int)(dealer.Attack  * ((level - 1) * opSkill.Damage.BonusPerLevel + opSkill.Damage.Scaling) / 100);
            if(opSkill.Damage.SourceDamage == EquipStatType.MagicAttack)
                return (int)(dealer.MagicAttack * ((level - 1) * opSkill.Damage.BonusPerLevel + opSkill.Damage.Scaling) / 100);
        }

        return 0;
    }
}