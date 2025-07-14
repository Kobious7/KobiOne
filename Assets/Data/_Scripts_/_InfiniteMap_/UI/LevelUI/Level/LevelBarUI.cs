using System;
using TMPro;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.UI;

public class LevelBarUI : GMono
{
    [SerializeField] private Image slider;
    [SerializeField] private TextMeshProUGUI percent;
    [SerializeField] private TextMeshProUGUI level;
    [SerializeField] private float lerpSpeed = 2f;
    private InfiniteMapManager infiniteMapManager;
    private PlayerInfo playerInfo;
    private CharacterSO characterData;
    private IMPlayerStats playerStats;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        slider = transform.Find("Slider").GetComponent<Image>();
        percent = transform.Find("Percent").GetComponent<TextMeshProUGUI>();
        level = transform.Find("Level").GetComponentInChildren<TextMeshProUGUI>();
    }

    protected override void Start()
    {
        base.Start();

        infiniteMapManager = InfiniteMapManager.Instance;
        playerInfo = infiniteMapManager.MapData.PlayerInfo;
        characterData = infiniteMapManager.CharacterData;
        playerStats = infiniteMapManager.Player.StatsSystem;
        int currentExp, maxExp;

        if (infiniteMapManager.MapData.MapCanLoad)
        {
            level.text = $"{playerInfo.Level}";
            currentExp = playerInfo.CurrentExp;
            maxExp = playerInfo.RequiredExp;
            percent.text = currentExp > 0 ? $"{((float)(currentExp / maxExp) * 100):F2}%" : "0%";
            slider.fillAmount = (float)(currentExp / maxExp);
        }
        else
        {
            level.text = $"{characterData.Level}";
            currentExp = characterData.CurrentExp;
            int averageMonsters = (characterData.Level / 10) * 10 + characterData.Level;
            maxExp = ((characterData.Level / 10) * 10 + 10) * averageMonsters;
        }

        percent.text = currentExp > 0 ? $"{((float)(currentExp / maxExp) * 100):F2}%" : "0%";
        slider.fillAmount = (float)(currentExp / maxExp);
    }

    private void Update()
    {
        level.text = $"{playerStats.Level}";
        percent.text = playerStats.CurrentExp > 0 ? $"{((float)playerStats.CurrentExp / playerStats.RequiredExp * 100):F2}%" : "0%";
        if (playerStats.LerpExpStack > 0)
        {
            slider.fillAmount = Mathf.Lerp(slider.fillAmount, 1, lerpSpeed * Time.deltaTime);

            if (slider.fillAmount >= 0.99f)
            {
                slider.fillAmount = 0;
                playerStats.LerpExpStack--;
            }
        }
        else
        {
            slider.fillAmount = Mathf.Lerp(slider.fillAmount, (float)playerStats.CurrentExp / playerStats.RequiredExp, lerpSpeed * Time.deltaTime);

            if (slider.fillAmount >= (float)playerStats.CurrentExp / playerStats.RequiredExp - 0.01f)
            {
                slider.fillAmount = (float)playerStats.CurrentExp / playerStats.RequiredExp;
            }
        }
    }
}