using UnityEngine;
using UnityEngine.UI;

public class QuitBattleUI : GMono
{
    [SerializeField] private Button quitBtn;
    [SerializeField] private QuitBattlePromptUI promptUI;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        if (quitBtn == null) quitBtn = transform.Find("QuitBtn").GetComponent<Button>();
        if (promptUI == null) promptUI = GetComponentInChildren<QuitBattlePromptUI>();
    }

    protected override void Start()
    {
        base.Start();

        quitBtn.onClick.AddListener(OpenPromptClickListener);
    }

    private void OpenPromptClickListener()
    {
        promptUI.gameObject.SetActive(true);
    }
}