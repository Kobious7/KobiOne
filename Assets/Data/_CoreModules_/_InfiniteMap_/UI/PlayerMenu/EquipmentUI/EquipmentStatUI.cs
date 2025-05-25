using TMPro;
using UnityEngine;

public class EquipmentStatUI : GMono
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI value;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadText();
        LoadValue();
    }

    private void LoadText()
    {
        if(text != null) return;

        text = transform.Find("Text").GetComponent<TextMeshProUGUI>();
    }

    private void LoadValue()
    {
        if(value != null) return;

        value = transform.Find("Value").GetComponent<TextMeshProUGUI>();
    }

    public void Show(EquipStat stat)
    {
        if(stat.TypeBonus == TypeBonus.FlatBonus)
        {
            if(stat.FlatValue > 0)
            {
                text.text = GetStatString(stat);
                value.text = $"{stat.FlatValue}";
            }
            else
            {
                text.text = "";
                value.text = "";
            }
        }
        else
        {
            if(stat.PercentValue > 0)
            {
                text.text = GetStatString(stat);
                value.text = $"{stat.PercentValue:F1}%";
            }
            else
            {
                text.text = "";
                value.text = "";
            }
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