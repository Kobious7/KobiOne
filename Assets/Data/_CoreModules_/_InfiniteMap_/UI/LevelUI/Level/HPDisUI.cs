using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPDisUI : GMono
{
    [SerializeField] private TextMeshProUGUI hpText;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadHPText();
    }

    private void FixedUpdate()
    {
        hpText.text = $"{Game.Instance.Player.InfiniteMapStats.Level * 100}";
    }

    private void LoadHPText()
    {
        if(hpText != null) return;

        hpText = transform.Find("HPText").GetComponent<TextMeshProUGUI>();
    }
}