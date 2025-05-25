using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SkillBtnUI : GMono
{
    [SerializeField] protected Button button;
    [SerializeField] protected Image model;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadButton();
        LoadModel();
    }

    protected override void Start()
    {
        base.Start();

        StartCoroutine(WaitNexFrame());
    }

    private void FixedUpdate()
    {
        UpdateButtonUnlocking();
    }

    private IEnumerator WaitNexFrame()
    {
        yield return null;

        ShowSkill();
    }

    private void LoadButton()
    {
        if(button != null) return;

        button = GetComponent<Button>();
    }

    private void LoadModel()
    {
        if(model != null) return;

        model = transform.Find("Model").GetComponent<Image>();
    }

    private void OnButtonClick()
    {
        Click();
    }

    protected virtual void Click()
    {
        //Override
    }

    private void ShowSkill()
    {
        SkillNode skill = GetSkill();

        if(skill.Level <= 0)
        {
            transform.gameObject.SetActive(false);
        }
        else
        {
            model.sprite = skill.skillSO.SkillIcon;
            button.onClick.AddListener(OnButtonClick);
        }
    }

    private void UpdateButtonUnlocking()
    {
        if(!GetManaCost())
        {
            button.interactable = false;
            model.color = Color.gray;
        }
        else
        {
            button.interactable = true;
            model.color = Color.white;
        }
    }

    protected virtual bool GetManaCost()
    {
        return false; //Override
    }

    protected virtual SkillNode GetSkill()
    {
        return null; //Override
    }
}