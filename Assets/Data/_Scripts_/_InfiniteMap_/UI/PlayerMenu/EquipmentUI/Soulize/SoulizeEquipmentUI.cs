using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoulizeEquipmentUI : GMono
{
    private static SoulizeEquipmentUI instance;

    public static SoulizeEquipmentUI Instance => instance;

    public event Action OnSoulizeComplete;

    [SerializeField] private Button transBG, closeBtn, soulizeBtn;
    [SerializeField] private Image quality, equipImage;
    [SerializeField] private TextMeshProUGUI soulColledted;

    protected override void Awake()
    {
        if (instance != null) Debug.LogError("Only 1 SoulizeEquipmentUI");

        instance = this;

        this.gameObject.SetActive(false);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (transBG == null) transBG = transform.Find("TransBG").GetComponent<Button>();
        if (closeBtn == null) closeBtn = transform.Find("CloseBtn").GetComponent<Button>();
        if (soulizeBtn == null) soulizeBtn = transform.Find("SoulizeBtn").GetComponent<Button>();
        if (quality == null) quality = transform.Find("Equip").GetComponent<Image>();
        if (equipImage == null) equipImage = transform.Find("Equip").Find("Image").GetComponent<Image>();
        if (soulColledted == null) soulColledted = transform.Find("SoulCollected").GetComponent<TextMeshProUGUI>();
    }

    public void SetSoulizeEquipmentUI(InventoryEquip equip)
    {
        transBG.onClick.RemoveAllListeners();
        closeBtn.onClick.RemoveAllListeners();
        soulizeBtn.onClick.RemoveAllListeners();

        quality.color = GetQualityColorByRarity(equip.Rarity);
        equipImage.sprite = equip.EquipSO.Sprite;

        transBG.onClick.AddListener(CloseClickListener);
        closeBtn.onClick.AddListener(CloseClickListener);

        soulColledted.text = $"{GetPrimarionSoulByRarity(equip.Rarity)}";

        soulizeBtn.onClick.AddListener(() => SoulizeClickListener(equip));
    }

    private void CloseClickListener()
    {
        this.gameObject.SetActive(false);
    }

    private void SoulizeClickListener(InventoryEquip equip)
    {
        InfiniteMapManager.Instance.Inventory.Soulize.SoulizeEquipment(equip);
        this.gameObject.SetActive(false);
        OnSoulizeComplete?.Invoke();
    }

    private int GetPrimarionSoulByRarity(Rarity rarity)
    {
        return rarity switch
        {
            Rarity.Common => 2,
            Rarity.Uncommon => 5,
            Rarity.Rare => 10,
            Rarity.Epic => 30,
            Rarity.Lengendary => 70,
            Rarity.Mythic => 200,
            Rarity.Divine => 1000,
            Rarity.Infinity => 999999999,
            _ => 0
        };
    }
}