using TMPro;
using UnityEngine;

public class CurrentLevelDetailUI : GMono
{
    [SerializeField] private TextMeshProUGUI skillName;
    [SerializeField] private TextMeshProUGUI level;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI mana;
    [SerializeField] private SkillTreeTextFormatter formatter;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadName();
        LoadLevel();
        LoadDescription();
        LoadMana();
        LoadFormatter();
    }

    private void LoadName()
    {
        if(skillName != null) return;

        skillName = transform.Find("Name").GetComponent<TextMeshProUGUI>();
    }

    private void LoadLevel()
    {
        if(level != null) return;

        level = transform.Find("Level").GetComponent<TextMeshProUGUI>();
    }

    private void LoadDescription()
    {
        if(description != null) return;

        description = transform.Find("Description").GetComponent<TextMeshProUGUI>();
    }

    private void LoadMana()
    {
        if(mana != null) return;

        mana = transform.Find("Mana").GetComponent<TextMeshProUGUI>();
    }

    private void LoadFormatter()
    {
        if(formatter != null) return;

        formatter = GetComponentInChildren<SkillTreeTextFormatter>();
    }

    public void ShowCurrentLevel(SkillNode skill)
    {
        ClearData();

        skillName.text = skill.SkillSO.SkillName;
        
        if(skill.Level <= 0) return;

        level.text = $"Level: {skill.Level}";
        description.text = formatter.GetReplacedDescription(skill, skill.Level);

        level.gameObject.SetActive(true);
        description.gameObject.SetActive(true);

        if(skill.SkillSO is ActiveSkillSO)
        {
            ActiveSkillSO activeSkll = (ActiveSkillSO)skill.SkillSO;
            mana.text = $"Mana cost: {activeSkll.ManaCost}";

            mana.gameObject.SetActive(true);
        }

    }

    private void ClearData()
    {
        level.text = "";
        description.text = "";
        mana.text = "";

        level.gameObject.SetActive(false);
        description.gameObject.SetActive(false);
        mana.gameObject.SetActive(false);
    }
}