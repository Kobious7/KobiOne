using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class skillPointsUI : GMono
{
    [SerializeField] private TextMeshProUGUI skillPointsText;
    [SerializeField] private Button resetBtn;
    [SerializeField] private ResetPromptUI resetPromptUI;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        skillPointsText = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        resetBtn = transform.Find("ResetBtn").GetComponent<Button>();
        resetPromptUI = GetComponentInChildren<ResetPromptUI>();
    }

    protected override void Start()
    {
        base.Start();
        StartCoroutine(WaitNextFrame());
    }

    private IEnumerator WaitNextFrame()
    {
        yield return null;
        ShowSkillPointsUI();

        SkillSGT.Instance.Skill.OnSkillPointChanging += ShowSkillPointsUI;
    }

    private void ShowSkillPointsUI()
    {
        skillPointsText.text = $"{SkillSGT.Instance.Skill.SkillPoints}";

        resetBtn.onClick.RemoveAllListeners();
        resetBtn.onClick.AddListener(ClickToOpenPromptListener);
    }

    private void ClickToOpenPromptListener()
    {
        resetPromptUI.gameObject.SetActive(true);
    }
}