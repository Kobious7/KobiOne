using UnityEngine;
using UnityEngine.UI;

public class NewGamePromptUI : GMono
{
    [SerializeField] private Button promptBtn, yesBtn, noBtn;

    protected override void LoadComponents()
    {
        promptBtn = GetComponent<Button>();
        yesBtn = transform.Find("YesBtn").GetComponent<Button>();
        noBtn = transform.Find("NoBtn").GetComponent<Button>();
    }

    protected override void Start()
    {
        base.Start();

        promptBtn.onClick.AddListener(CloseClickListener);
        noBtn.onClick.AddListener(CloseClickListener);
        yesBtn.onClick.AddListener(YesClickListener);

        this.gameObject.SetActive(false);
    }

    private void CloseClickListener()
    {
        this.gameObject.SetActive(false);
    }

    private void YesClickListener()
    {
        SavingManager.Instance.ResetDataToDefault();
        LoadScene(INFINITEMAP);
    }
}