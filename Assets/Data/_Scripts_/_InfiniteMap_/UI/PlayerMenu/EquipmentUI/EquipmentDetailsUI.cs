using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentDetailsUI : GMono
{
    private static EquipmentDetailsUI instance;

    public static EquipmentDetailsUI Instance => instance;

    [SerializeField] private Image qualityColor, model;
    [SerializeField] private TextMeshProUGUI equipName;
    [SerializeField] private EquipmentStatUI mainStat;
    [SerializeField] private TextMeshProUGUI level;
    [SerializeField] private EquipmentSubStatsSpawner subStats;
    [SerializeField] private Button closeBtn, equipBtn, unequipBtn, upgradeBtn, soulizeBtn, lockBtn, unlockBtn;

    private CurrentEquipmentUI currentEquipmentUI;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.LogError("Only 1 EquipmentDetailsUI is allowed to exist!");

        instance = this;
        transform.gameObject.SetActive(false);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadQualityColor();
        LoadModel();
        LoadEquipName();
        LoadMainStat();
        LoadLevel();
        LoadSubStats();
        LoadCloseButton();
        LoadEquipButton();
        LoadUnequipButton();

        if (upgradeBtn == null) upgradeBtn = transform.Find("Buttons").Find("ButtonLine1").Find("UpgradeBtn").GetComponent<Button>();
        if (soulizeBtn == null) soulizeBtn = transform.Find("Buttons").Find("ButtonLine2").Find("SoulizeBtn").GetComponent<Button>();
        if (lockBtn == null) lockBtn = transform.Find("Lock").Find("LockBtn").GetComponent<Button>();
        if (unlockBtn == null) unlockBtn = transform.Find("Lock").Find("UnlockBtn").GetComponent<Button>();
    }

    protected override void Start()
    {
        base.Start();
        currentEquipmentUI = GameUI.Instance.CurrentEquipmentUI;
        closeBtn.onClick.AddListener(Click);
    }

    private void LoadQualityColor()
    {
        if (qualityColor != null) return;

        qualityColor = transform.Find("BG").GetComponent<Image>();
    }

    private void LoadModel()
    {
        if (model != null) return;

        model = transform.Find("Model").GetComponent<Image>();
    }

    private void LoadEquipName()
    {
        if (equipName != null) return;

        equipName = transform.Find("Name").GetComponent<TextMeshProUGUI>();
    }

    private void LoadMainStat()
    {
        if (mainStat != null) return;

        mainStat = transform.Find("MainStat").GetComponent<EquipmentStatUI>();
    }

    private void LoadLevel()
    {
        if (level != null) return;

        level = transform.Find("Level").Find("Value").GetComponent<TextMeshProUGUI>();
    }

    private void LoadSubStats()
    {
        if (subStats != null) return;

        subStats = transform.Find("SubStats").GetComponent<EquipmentSubStatsSpawner>();
    }

    private void LoadCloseButton()
    {
        if (closeBtn != null) return;

        closeBtn = transform.Find("CloseBtn").GetComponent<Button>();
    }

    private void LoadEquipButton()
    {
        if (equipBtn != null) return;

        equipBtn = transform.Find("Buttons").Find("ButtonLine1").Find("EquipBtn").Find("EquipBtn").GetComponent<Button>();
    }

    private void LoadUnequipButton()
    {
        if (unequipBtn != null) return;

        unequipBtn = transform.Find("Buttons").Find("ButtonLine1").Find("EquipBtn").Find("UnequipBtn").GetComponent<Button>();
    }

    public void ShowDetails(InventoryEquip equip, bool isCurrentEquip = false)
    {
        qualityColor.color = GetQualityColorByRarity(equip.Rarity);
        model.sprite = equip.EquipSO.Sprite;
        equipName.text = equip.EquipSO.ItemName;
        level.text = $"{equip.Level}";
        mainStat.Show(equip.MainStat);
        subStats.SpawnSubStats(equip.SubStats);
        equipBtn.onClick.RemoveAllListeners();
        unequipBtn.onClick.RemoveAllListeners();
        upgradeBtn.onClick.RemoveAllListeners();
        upgradeBtn.onClick.AddListener(() => UpgradeClickListener(equip));

        if (equip.IsLock)
        {
            soulizeBtn.transform.parent.gameObject.SetActive(false);
            lockBtn.gameObject.SetActive(true);
            unlockBtn.gameObject.SetActive(false);
        }
        else
        {
            soulizeBtn.transform.parent.gameObject.SetActive(true);
            soulizeBtn.onClick.RemoveAllListeners();
            soulizeBtn.onClick.AddListener(() => SoulizeClickListener(equip));
            lockBtn.gameObject.SetActive(false);
            unlockBtn.gameObject.SetActive(true);
        }

        lockBtn.onClick.RemoveAllListeners();
        unlockBtn.onClick.RemoveAllListeners();
        lockBtn.onClick.AddListener(() => UnlockClickListener(equip));
        unlockBtn.onClick.AddListener(() => LockClickListener(equip));
    }

    public void AddEquipClickListener(InventoryEquip equip)
    {
        unequipBtn.transform.parent.gameObject.SetActive(true);
        unequipBtn.gameObject.SetActive(false);
        equipBtn.gameObject.SetActive(true);
        equipBtn.onClick.AddListener(() => EquipClickListener(equip));
    }

    public void AddUnequipClickListener(InventoryEquip equip)
    {
        unequipBtn.transform.parent.gameObject.SetActive(true);
        unequipBtn.gameObject.SetActive(true);
        equipBtn.gameObject.SetActive(false);
        unequipBtn.onClick.AddListener(() => UnequipClickListener(equip));
    }

    private void Click()
    {
        if (currentEquipmentUI.CurrentEquip != null)
        {
            currentEquipmentUI.CurrentEquip.OnSelected.gameObject.SetActive(false);
            currentEquipmentUI.CurrentEquip = null;
        }

        if (currentEquipmentUI.CurrentSpawner != null)
        {
            if (currentEquipmentUI.CurrentSpawner.CurrentEquip != null)
            {
                currentEquipmentUI.CurrentSpawner.CurrentEquip.OnSelected.gameObject.SetActive(false);
                currentEquipmentUI.CurrentSpawner.CurrentEquip = null;
            }

            currentEquipmentUI.CurrentSpawner.gameObject.SetActive(false);
            currentEquipmentUI.CurrentSpawner = null;
        }

        transform.gameObject.SetActive(false);
    }

    private void EquipClickListener(InventoryEquip equip)
    {
        InfiniteMapManager.Instance.Inventory.EquipWearing.Equip(equip);
        unequipBtn.transform.parent.gameObject.SetActive(false);
        soulizeBtn.transform.parent.gameObject.SetActive(false);
    }

    private void UnequipClickListener(InventoryEquip equip)
    {
        InfiniteMapManager.Instance.Equipment.Unequip.Unequip(equip);
        unequipBtn.transform.parent.gameObject.SetActive(false);
    }

    private void UpgradeClickListener(InventoryEquip equip)
    {
        EquipmentUpgradeUI.Instance.gameObject.SetActive(true);
        EquipmentUpgradeUI.Instance.SetEquipmentUpgradeUI(equip);
    }

    private void SoulizeClickListener(InventoryEquip equip)
    {
        SoulizeEquipmentUI.Instance.gameObject.SetActive(true);
        SoulizeEquipmentUI.Instance.SetSoulizeEquipmentUI(equip);
    }

    private void LockClickListener(InventoryEquip equip)
    {
        equip.IsLock = true;
        lockBtn.gameObject.SetActive(true);
        unlockBtn.gameObject.SetActive(false);
    }

    private void UnlockClickListener(InventoryEquip equip)
    {
        equip.IsLock = false;
        lockBtn.gameObject.SetActive(false);
        unlockBtn.gameObject.SetActive(true);
        soulizeBtn.transform.parent.gameObject.SetActive(true);
        soulizeBtn.onClick.RemoveAllListeners();
        soulizeBtn.onClick.AddListener(() => SoulizeClickListener(equip));
    }
}