using UnityEngine;
using UnityEngine.UI;

public class QuitBattlePromptUI : GMono
{
    [SerializeField] private Button button, noBtn, yesBtn;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        if (button == null) button = GetComponent<Button>();
        if (noBtn == null) noBtn = transform.Find("NoBtn").GetComponent<Button>();
        if (yesBtn == null) yesBtn = transform.Find("YesBtn").GetComponent<Button>();
    }

    protected override void Start()
    {
        base.Start();

        button.onClick.AddListener(CloseClickListener);
        noBtn.onClick.AddListener(CloseClickListener);
        yesBtn.onClick.AddListener(YesClockListener);

        this.gameObject.SetActive(false);
    }

    private void CloseClickListener()
    {
        this.gameObject.SetActive(false);
    }

    private void YesClockListener()
    {
        BattleManager.Instance.MapData.Result = Result.NONE;
        LoadScene(INFINITEMAP);
    }
}