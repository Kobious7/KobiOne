using UnityEngine;
using UnityEngine.UI;

public abstract class ResetPromptUI : GMono
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

    protected virtual void ClickToResetListener() {}
}