using UnityEngine;
using UnityEngine.UI;

public class SkillDetailSVUI : GMono
{
    [SerializeField] private CurrentLevelDetailUI currentLevel;
    [SerializeField] private NextLevelDetailUI nextLevel;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadLevel();
    }

    private void LoadLevel()
    {
        if(nextLevel != null && currentLevel != null) return;

        currentLevel = GetComponent<ScrollRect>().content.GetComponentInChildren<CurrentLevelDetailUI>();
        nextLevel = GetComponent<ScrollRect>().content.GetComponentInChildren<NextLevelDetailUI>();
    }

    public void ShowText(SkillNode skill)
    {
        currentLevel.ShowCurrentLevel(skill);
        nextLevel.ShowNextLevel(skill);
    }
}