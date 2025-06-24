using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : GMono
{
    [SerializeField] private TextMeshProUGUI levelText;

    public TextMeshProUGUI LevelText => levelText;

    [SerializeField] private TextMeshProUGUI levelPercentText;

    public TextMeshProUGUI LevelPercentText => levelPercentText;

    [SerializeField] private UnityEngine.UI.Image levelPercent;

    public Image LevelPercent => levelPercent;

    private float speed = 2;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadLevelText();
        LoadLevelPercentText();
        LoadLevelPercent();
    }

    private void Update()
    {
        UpdateLevel(InfiniteMapManager.Instance.Player);
    }

    private void LoadLevelText()
    {
        if(levelText != null) return;

        levelText = transform.Find("LevelText").GetComponent<TextMeshProUGUI>();
    }

    private void LoadLevelPercentText()
    {
        if(levelPercentText != null) return;

        levelPercentText = transform.Find("LevelPercentText").GetComponent<TextMeshProUGUI>();
    }

    private void LoadLevelPercent()
    {
        if(levelPercent != null) return;

        levelPercent = transform.Find("LevelPercent").GetComponent<Image>();
    }

    public void UpdateLevel(IMPlayer player)
    {
        int level = InfiniteMapManager.Instance.CharacterData.Level;
        int current = player.StatsSystem.CurrentExp;
        int max = player.StatsSystem.RequiredExp;
        levelText.text = level + "";
        levelPercentText.text = current > 0 ? $"{(((float)current / max) * 100):F2}%" : "0%";
        levelPercent.fillAmount = Mathf.Lerp(levelPercent.fillAmount, (float)current/max, speed * Time.deltaTime);
    }
}