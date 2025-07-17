using UnityEngine;
using UnityEngine.UI;

public class SkillNodeUI : GMono
{
    [SerializeField] private Button button;
    [SerializeField] private Image model;

    private string hexacolor = "#313131";

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadButton();
        LoadModel();
    }

    private void LoadButton()
    {
        if(button != null) return;

        button = GetComponent<Button>();
    }

    private void LoadModel()
    {
        if(model != null) return;

        model = transform.Find("Image").GetComponent<Image>();
    }

    public void ShowSkill(SkillNode skill, int treeIndex, bool treeActive)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => Click(skill, treeIndex, treeActive));
        model.sprite = skill.skillSO.SkillIcon;

        if(skill.Level > 0)
        {
            model.color = Color.white;
        }
        else
        {
            model.color = GetColor(hexacolor);
        }
    }

    public void UpdateClick(SkillNode skill, int treeIndex, bool treeActive)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => Click(skill, treeIndex, treeActive));
    }

    private void Click(SkillNode skill, int treeIndex, bool treeActive)
    {
        SkillDetailUI.Instance.ShowSkilDetailsUI(skill, treeIndex, treeActive);
    }

    public Color GetColor(string hexastring)
    {
        return ColorUtility.TryParseHtmlString(hexastring, out Color color) ? color : Color.white;
    }
}