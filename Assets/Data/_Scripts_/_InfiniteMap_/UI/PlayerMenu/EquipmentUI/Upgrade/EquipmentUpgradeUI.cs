using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUpgradeUI : GMono
{
    private static EquipmentUpgradeUI instance;
    public static EquipmentUpgradeUI Instance => instance;

    public event Action<InventoryEquip> OnUpgradeComplete;

    [SerializeField] private Image quality;
    [SerializeField] private Image image;
    [SerializeField] private Button upgradeBtn;
    [SerializeField] private UpgradeStatLineUI levelLine, mainStatLine;
    [SerializeField] private UpgradeSubstatLineUI[] substatLines;
    [SerializeField] private RequireSoulUI requireSoul;
    private EquipObtainer equipObtainer;

    protected override void Awake()
    {
        if (instance != null) Debug.LogError("Only 1 EquipmentUpgradeUI");

        instance = this;

        gameObject.SetActive(false);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();

        quality = transform.Find("Quality").GetComponent<Image>();
        image = transform.Find("Image").GetComponent<Image>();
        upgradeBtn = transform.Find("UpgradeBtn").GetComponent<Button>();
        levelLine = transform.Find("Level").GetComponent<UpgradeStatLineUI>();
        mainStatLine = transform.Find("MainStat").GetComponent<UpgradeStatLineUI>();
        substatLines = GetComponentsInChildren<UpgradeSubstatLineUI>();
        requireSoul = GetComponentInChildren<RequireSoulUI>();
    }

    public void SetEquipmentUpgradeUI(InventoryEquip equip)
    {
        quality.color = GetQualityColorByRarity(equip.Rarity);
        image.sprite = equip.EquipSO.Sprite;

        levelLine.SetLevelLine(equip.Level);

        equipObtainer = InfiniteMapManager.Instance.Inventory.EquipObtainer;
        EquipStatBonus defaultRarityBonus = equipObtainer.GetDefalutRarityBounusByRarity(equip.Rarity);
        mainStatLine.SetMainStatLine(equip.MainStat, defaultRarityBonus);

        for (int i = 0; i < substatLines.Length; i++)
        {
            if (i < equip.SubStats.Count)
            {
                substatLines[i].SetSubstatLine(equip.SubStats[i], defaultRarityBonus);
            }
            else
            {
                substatLines[i].SetSubstatLine(null, defaultRarityBonus);
            }
        }

        requireSoul.SetRequireSoul(InfiniteMapManager.Instance.Inventory, equip);

        upgradeBtn.onClick.RemoveAllListeners();

        if (InfiniteMapManager.Instance.Inventory.PrimarionSoul >= equip.CurrentUpgradeCost)
        {
            upgradeBtn.interactable = true;
            upgradeBtn.onClick.AddListener(() => UpgradeClickListener(equipObtainer, equip));
        }
        else
        {
            upgradeBtn.interactable = false;
        }
    }

    public void UpgradeClickListener(EquipObtainer equipObtainer, InventoryEquip equip)
    {
        equipObtainer.CheckPrimarionSoulAndUpgrading(equip);
        SetEquipmentUpgradeUI(equip);
        OnUpgradeComplete?.Invoke(equip);
    }
}