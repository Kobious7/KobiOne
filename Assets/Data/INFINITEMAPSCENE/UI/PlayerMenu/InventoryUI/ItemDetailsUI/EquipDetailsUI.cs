using System.Collections.Generic;
using InfiniteMap;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipDetailsUI : GMono
{
    private static EquipDetailsUI instance;

    public static EquipDetailsUI Instance => instance;

    [SerializeField] private Image model;
    [SerializeField] private TextMeshProUGUI equipName;
    [SerializeField] private Image qualityColor;
    [SerializeField] private TextMeshProUGUI level;
    [SerializeField] private EquipmentStatUI mainStat;
    [SerializeField] private EquipmentSubStatsSpawner subStats;
    [SerializeField] private Button equipBtn;
    [SerializeField] private Vector3 initPos;

    protected override void Awake()
    {
        base.Awake();
        if(instance != null) Debug.LogError("Only 1 EquipDetailsUI is allowed to exist!");

        instance = this;
        instance.gameObject.SetActive(false);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadQualityColor();
        LoadModel();
        LoadEquipName();
        LoadLevel();
        LoadMainStat();
        LoadSubStats();
        LoadEquipBtn();
    }

    private void LoadQualityColor()
    {
        if(qualityColor != null) return;

        qualityColor = transform.Find("Details").Find("Quality").GetComponent<Image>();
    }

    private void LoadModel()
    {
        if(model != null) return;

        model = transform.Find("Details").Find("Model").GetComponent<Image>();
    }
    private void LoadEquipName()
    {
        if(equipName != null) return;

        equipName = transform.Find("Details").Find("Name").GetComponent<TextMeshProUGUI>();
    }

    private void LoadLevel()
    {
        if(level != null) return;

        level = transform.Find("Details").Find("Level").Find("Value").GetComponent<TextMeshProUGUI>();
    }

    private void LoadMainStat()
    {
        if (mainStat != null) return;

        mainStat = transform.Find("Details").Find("MainStat").GetComponent<EquipmentStatUI>();
    }

    private void LoadSubStats()
    {
        if(subStats != null) return;

        subStats = transform.Find("Details").Find("SubStats").GetComponent<EquipmentSubStatsSpawner>();
    }

    private void LoadEquipBtn()
    {
        if(equipBtn != null) return;

        equipBtn = transform.Find("Details").Find("EquipBtn").GetComponent<Button>();
    }

    public void ShowDetails(InventoryEquip equip)
    {
        qualityColor.color = GetQualityColorByRarity(equip.Rarity);
        model.sprite = equip.EquipSO.Sprite;
        equipName.text = equip.EquipSO.ItemName;
        level.text = $"{equip.Level}";

        mainStat.Show(equip.MainStat);
        subStats.SpawnSubStats(equip.SubStats);
        equipBtn.onClick.AddListener(() => EquipButtonClick(equip));
    }

    private void EquipButtonClick(InventoryEquip equip)
    {
        
    }
}