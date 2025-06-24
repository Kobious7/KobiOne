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
    [SerializeField] private Button closeBtn, equipBtn, unequipBtn;

    private CurrentEquipmentUI currentEquipmentUI;

    protected override void Awake()
    {
        base.Awake();
        if(instance != null) Debug.LogError("Only 1 EquipmentDetailsUI is allowed to exist!");

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
    }

    protected override void Start()
    {
        base.Start();
        currentEquipmentUI = GameUI.Instance.CurrentEquipmentUI;
        closeBtn.onClick.AddListener(Click);
    }

    private void LoadQualityColor()
    {
        if(qualityColor != null) return;

        qualityColor = transform.Find("BG").GetComponent<Image>();
    }

    private void LoadModel()
    {
        if(model != null) return;

        model = transform.Find("Model").GetComponent<Image>();
    }

    private void LoadEquipName()
    {
        if(equipName != null) return;

        equipName = transform.Find("Name").GetComponent<TextMeshProUGUI>();
    }

    private void LoadMainStat()
    {
        if(mainStat != null) return;

        mainStat = transform.Find("MainStat").GetComponent<EquipmentStatUI>();
    }

    private void LoadLevel()
    {
        if(level != null) return;

        level = transform.Find("Level").Find("Value").GetComponent<TextMeshProUGUI>();
    }

    private void LoadSubStats()
    {
        if(subStats != null) return;

        subStats = transform.Find("SubStats").GetComponent<EquipmentSubStatsSpawner>();
    }

    private void LoadCloseButton()
    {
        if(closeBtn != null) return;

        closeBtn = transform.Find("CloseBtn").GetComponent<Button>();
    }

    private void LoadEquipButton()
    {
        if(equipBtn != null) return;

        equipBtn = transform.Find("EquipBtn").GetComponent<Button>();
    }

    private void LoadUnequipButton()
    {
        if(unequipBtn != null) return;

        unequipBtn = transform.Find("UnequipBtn").GetComponent<Button>();
    }

    public void ShowDetails(InventoryEquip equip)
    {
        qualityColor.color = GetQualityColorByRarity(equip.Rarity);
        model.sprite = equip.EquipSO.Sprite;
        equipName.text = equip.EquipSO.ItemName;
        level.text = $"{equip.Level}";
        mainStat.Show(equip.MainStat);
        subStats.SpawnSubStats(equip.SubStats);
        equipBtn.onClick.RemoveAllListeners();
    }

    public void AddEquipClickListener(InventoryEquip equip)
    {
        unequipBtn.gameObject.SetActive(false);
        equipBtn.gameObject.SetActive(true);
        equipBtn.onClick.AddListener(() => EquipClickListener(equip));
    }

    public void AddUnequipClickListener(InventoryEquip equip)
    {
        unequipBtn.gameObject.SetActive(true);
        equipBtn.gameObject.SetActive(false);
        unequipBtn.onClick.AddListener(() => UnequipClickListener(equip));
    }

    private void Click()
    {
        if(currentEquipmentUI.CurrentEquip != null)
        {
            currentEquipmentUI.CurrentEquip.OnSelected.gameObject.SetActive(false);
            currentEquipmentUI.CurrentEquip = null;
        }

        if(currentEquipmentUI.CurrentSpawner != null)
        {
            if(currentEquipmentUI.CurrentSpawner.CurrentEquip != null)
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
        equipBtn.gameObject.SetActive(false);
        unequipBtn.gameObject.SetActive(false);
    }

    private void UnequipClickListener(InventoryEquip equip)
    {
        InfiniteMapManager.Instance.Equipment.Unequip.Unequip(equip);
        equipBtn.gameObject.SetActive(false);
        unequipBtn.gameObject.SetActive(false);
    }
}