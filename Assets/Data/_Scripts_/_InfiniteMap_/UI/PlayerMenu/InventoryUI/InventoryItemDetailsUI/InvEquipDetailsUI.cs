using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InvEquipDetailsUI : GMono
{
    private static InvEquipDetailsUI instance;

    public static InvEquipDetailsUI Instance => instance;

    [SerializeField] private Transform details;
    [SerializeField] private Image quality, image;
    [SerializeField] private TextMeshProUGUI equipName;
    [SerializeField] private TextMeshProUGUI level;
    [SerializeField] private EquipmentStatUI mainStat;
    [SerializeField] private EquipmentSubStatsSpawner subStats;
    [SerializeField] private Button equipBtn, upgradeBtn, soulizeBtn, lockBtn, unlockBtn;
    [SerializeField] private Vector3 initPos;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.LogError("Only have 1 InvEquipDetailsUI instance!");

        instance = this;
        instance.gameObject.SetActive(false);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();

        if (details == null) details = transform.Find("Details");
        if (quality == null) quality = details.Find("Quality").GetComponent<Image>();
        if (image == null) image = details.Find("Image").GetComponent<Image>();
        if (equipName == null) equipName = details.Find("Name").GetComponent<TextMeshProUGUI>();
        if (level == null) level = details.Find("Level").Find("Value").GetComponent<TextMeshProUGUI>();
        if (mainStat == null) mainStat = details.Find("MainStat").GetComponent<EquipmentStatUI>();
        if (subStats == null) subStats = details.Find("SubStats").GetComponent<EquipmentSubStatsSpawner>();
        if (equipBtn == null) equipBtn = details.Find("Buttons").Find("ButtonLine1").Find("EquipBtn").GetComponent<Button>();
        if (upgradeBtn == null) upgradeBtn = details.Find("Buttons").Find("ButtonLine1").Find("UpgradeBtn").GetComponent<Button>();
        if (soulizeBtn == null) soulizeBtn = details.Find("Buttons").Find("ButtonLine2").Find("SoulizeBtn").GetComponent<Button>();
        if (lockBtn == null) lockBtn = details.Find("Lock").Find("LockBtn").GetComponent<Button>();
        if (unlockBtn == null) unlockBtn = details.Find("Lock").Find("UnlockBtn").GetComponent<Button>();
    }

    protected override void Start()
    {
        base.Start();
        SoulizeEquipmentUI.Instance.OnSoulizeComplete += OffUI;
        EquipmentUpgradeUI.Instance.OnUpgradeComplete += ReshowStatsAndLevel;
    }

    private void OffUI()
    {
        this.gameObject.SetActive(false);
    }

    public void ShowDetails(InventoryItemUI inventoryItem)
    {
        InventoryEquip equip = (InventoryEquip)inventoryItem.InventoryItem;
        quality.color = GetQualityColorByRarity(equip.Rarity);
        image.sprite = equip.EquipSO.Sprite;
        equipName.text = equip.EquipSO.ItemName;
        level.text = $"{equip.Level}";

        mainStat.Show(equip.MainStat);
        subStats.SpawnSubStats(equip.SubStats);

        equipBtn.onClick.RemoveAllListeners();
        lockBtn.onClick.RemoveAllListeners();
        unlockBtn.onClick.RemoveAllListeners();
        soulizeBtn.onClick.RemoveAllListeners();
        upgradeBtn.onClick.RemoveAllListeners();
        equipBtn.onClick.AddListener(() => EquipClickListener(equip));
        lockBtn.onClick.AddListener(() => UnlockClickListener(inventoryItem));
        unlockBtn.onClick.AddListener(() => LockClickListener(inventoryItem));
        soulizeBtn.onClick.AddListener(() => SoulizeClickListener(equip));
        upgradeBtn.onClick.AddListener(() => UpgradeClickListener(equip));

        if (equip.IsLock)
        {
            soulizeBtn.transform.parent.gameObject.SetActive(false);
            unlockBtn.gameObject.SetActive(false);
            lockBtn.gameObject.SetActive(true);
        }
        else
        {
            soulizeBtn.transform.parent.gameObject.SetActive(true);
            lockBtn.gameObject.SetActive(false);
            unlockBtn.gameObject.SetActive(true);
        }
    }

    private void EquipClickListener(InventoryEquip equip)
    {
        InfiniteMapManager.Instance.Inventory.EquipWearing.Equip(equip);
        this.gameObject.SetActive(false);
    }

    private void LockClickListener(InventoryItemUI inventoryItem)
    {
        soulizeBtn.transform.parent.gameObject.SetActive(false);
        unlockBtn.gameObject.SetActive(false);
        lockBtn.gameObject.SetActive(true);

        ReverseLock(inventoryItem);
    }

    private void UnlockClickListener(InventoryItemUI inventoryItem)
    {
        soulizeBtn.transform.parent.gameObject.SetActive(true);
        unlockBtn.gameObject.SetActive(true);
        lockBtn.gameObject.SetActive(false);

        ReverseLock(inventoryItem);
    }

    private void ReverseLock(InventoryItemUI inventoryItem)
    {
        InvEquipUI equipUI = (InvEquipUI)inventoryItem;
        equipUI.ReverseLockAndShow();
        InventoryEquip equip = (InventoryEquip)equipUI.InventoryItem;
        NewAndLockEquip.Instance.OnNewOrLockChangedEventInvoke(equip, false);
    }

    private void SoulizeClickListener(InventoryEquip equip)
    {
        SoulizeEquipmentUI.Instance.gameObject.SetActive(true);
        SoulizeEquipmentUI.Instance.SetSoulizeEquipmentUI(equip);
    }

    private void UpgradeClickListener(InventoryEquip equip)
    {
        EquipmentUpgradeUI.Instance.gameObject.SetActive(true);
        EquipmentUpgradeUI.Instance.SetEquipmentUpgradeUI(equip);
    }

    private void ReshowStatsAndLevel(InventoryEquip equip)
    {
        level.text = $"{equip.Level}";
        mainStat.Show(equip.MainStat);
        subStats.SpawnSubStats(equip.SubStats);
    }
}