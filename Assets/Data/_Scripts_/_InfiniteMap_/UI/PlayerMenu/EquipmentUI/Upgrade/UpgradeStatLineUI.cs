using TMPro;
using UnityEngine;

public class UpgradeStatLineUI : GMono
{
    [SerializeField] private TextMeshProUGUI statText;
    [SerializeField] private TextMeshProUGUI oldValue;
    [SerializeField] private TextMeshProUGUI newValue;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        statText = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        oldValue = transform.Find("OldValue").GetComponent<TextMeshProUGUI>();
        newValue = transform.Find("NewValue").GetComponent<TextMeshProUGUI>();
    }

    public void SetLevelLine(int level)
    {
        statText.text = "Level:";
        oldValue.text = $"{level}";
        newValue.text = $"{level + 1}";
    }

    public void SetMainStatLine(EquipStat stat, EquipStatBonus defaultRarityBonus)
    {
        statText.text = $"{GetStatString(stat)}:";

        if (stat.TypeBonus == TypeBonus.FlatBonus)
        {
            oldValue.text = $"{stat.FlatValue}";
            newValue.text = $"{stat.FlatValue + defaultRarityBonus.MainStat.FlatBonus}";
        }
        else
        {
            oldValue.text = $"{stat.PercentValue:F2}%";
            newValue.text = $"{(stat.PercentValue + defaultRarityBonus.MainStat.PercentBonus):F2}%";
        }
    }

    public void SetSubstatLine(EquipStat stat, EquipStatBonus defaultRarityBonus)
    {
        statText.text = $"{GetStatString(stat)}:";

        if (stat.TypeBonus == TypeBonus.FlatBonus)
        {
            oldValue.text = $"{stat.FlatValue}";
            newValue.text = $"{stat.FlatValue + defaultRarityBonus.SubStat.FlatBonus}";
        }
        else
        {
            oldValue.text = $"{stat.PercentValue:F2}%";
            newValue.text = $"{(stat.PercentValue + defaultRarityBonus.SubStat.PercentBonus):F2}%";
        }
    }

    private string GetStatString(EquipStat stat)
    {
        if(stat.Stat == EquipStatType.Power) return "Power";
        else if(stat.Stat == EquipStatType.Magic) return "Magic";
        else if(stat.Stat == EquipStatType.Strength) return "Strength";
        else if(stat.Stat == EquipStatType.DefenseP) return "Defense";
        else if(stat.Stat == EquipStatType.Dexterity) return "Dexterity";
        else if(stat.Stat == EquipStatType.Attack) return "Attack";
        else if(stat.Stat == EquipStatType.MagicAttack) return "Magic Attack";
        else if(stat.Stat == EquipStatType.HP) return "HP";
        else if(stat.Stat == EquipStatType.Defense) return "Defense";
        else if(stat.Stat == EquipStatType.Accuracy) return "Accuracy";
        else if(stat.Stat == EquipStatType.DamageRange) return "Damage Range";
        else if(stat.Stat == EquipStatType.Speed) return "Speed";
        else if(stat.Stat == EquipStatType.CritRate) return "Crit Rate";
        else return "Crit Damage";
    }
}