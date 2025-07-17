using UnityEngine;
using UnityEngine.UI;

public class ResetPromptUI : GMono
{
    [SerializeField] private Button transBGBtn, resetBtn, noBtn;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        transBGBtn = GetComponent<Button>();
        resetBtn = transform.Find("ResetBtn").GetComponent<Button>();
        noBtn = transform.Find("NoBtn").GetComponent<Button>();
    }

    protected override void Start()
    {
        transBGBtn.onClick.AddListener(ClickToCloseListener);
        resetBtn.onClick.AddListener(ClickToResetListener);
        noBtn.onClick.AddListener(ClickToCloseListener);
        this.transform.gameObject.SetActive(false);
    }

    private void ClickToCloseListener()
    {
        this.transform.gameObject.SetActive(false);
    }

    private void ClickToResetListener()
    {
        InfiniteMapManager.Instance.Skill.ResetAllSkill();
        this.transform.gameObject.SetActive(false);
    }
}