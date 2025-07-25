using TMPro;
using UnityEngine;

public class BattleEntranceInfoUI : GMono
{
    [SerializeField] private TextMeshProUGUI _name, level;

    protected override void LoadComponents()
    {
        _name = transform.Find("Info").Find("Name").GetComponent<TextMeshProUGUI>();
        level = transform.Find("Info").Find("Level").GetComponent<TextMeshProUGUI>();
    }

    public void ShowInfo(string name, int level)
    {
        _name.text = name;
        this.level.text = $"{level}";
    }
}