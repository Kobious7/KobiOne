using TMPro;
using UnityEngine;

public class NextLevelDetailUI : GMono
{
    [SerializeField] private TextMeshProUGUI level;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private SkillTreeTextFormatter formatter;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadLevel();
        LoadDescription();
        LoadFormatter();
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

    private void LoadFormatter()
    {
        if(formatter != null) return;

        formatter = GetComponentInChildren<SkillTreeTextFormatter>();
    }

    public void ShowNextLevel(SkillNode skill)
    {
        ClearData();

        if(skill.Level <= 0)
        {
            level.text = "Locked";
            description.text = skill.SkillSO.LockDescription;
        }
        else if(skill.Level >= skill.SkillSO.MaxLevel)
        {
            level.text = "Max level";
        }
        else
        {
            level.text = "Next level";
            int nextLevel = skill.Level + 1;
            description.text = formatter.GetReplacedDescription(skill, nextLevel);
        }

        level.gameObject.SetActive(true);
        if(description.text != "") description.gameObject.SetActive(true);
    }

    private void ClearData()
    {
        level.text = "";
        description.text = "";

        level.gameObject.SetActive(false);
        description.gameObject.SetActive(false);
    }
}