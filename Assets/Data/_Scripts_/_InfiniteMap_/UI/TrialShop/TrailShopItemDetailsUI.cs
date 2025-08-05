using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrailShopItemDetailsUI : GMono
{
    private static TrailShopItemDetailsUI instance;

    public static TrailShopItemDetailsUI Instance => instance;

    [SerializeField] private Image quality, image;
    [SerializeField] private Button button, closeBtn, buyBtn, randomBtn, maxBtn, insBtn, desBtn;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TextMeshProUGUI quantity, hint;
    private Inventory inventory;
    private IMPlayer player;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.LogError("Only 1 TrailShopItemDetailsUI");

        instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();

        if (quality == null) quality = transform.Find("Quality").GetComponent<Image>();
        if (image == null) image = transform.Find("Image").GetComponent<Image>();
        if (button == null) button = GetComponent<Button>();
        if (closeBtn == null) closeBtn = transform.Find("CloseBtn").GetComponent<Button>();
        if (buyBtn == null) buyBtn = transform.Find("BuyBtn").GetComponent<Button>();
        if (quantity == null) quantity = buyBtn.GetComponentInChildren<TextMeshProUGUI>();
        if (randomBtn == null) randomBtn = transform.Find("RandomBtn").GetComponent<Button>();
        if (maxBtn == null) maxBtn = transform.Find("MaxBtn").GetComponent<Button>();
        if (insBtn == null) insBtn = transform.Find("InscreaseBtn").GetComponent<Button>();
        if (desBtn == null) desBtn = transform.Find("DescreaseBtn").GetComponent<Button>();
        if (inputField == null) inputField = GetComponentInChildren<TMP_InputField>();
        if (hint == null) hint = transform.Find("Hint").GetComponent<TextMeshProUGUI>();
    }

    protected override void Start()
    {
        base.Start();

        inventory = InfiniteMapManager.Instance.Inventory;
        player = InfiniteMapManager.Instance.Player;

        this.gameObject.SetActive(false);
    }

    public void SetTrailShopItemDetails(TrailShopItem item)
    {
        quality.color = GetQualityColorByRarity(item.Quality);
        image.sprite = item.BaseItemSO.Sprite;

        button.onClick.RemoveAllListeners();
        closeBtn.onClick.RemoveAllListeners();
        insBtn.onClick.RemoveAllListeners();
        desBtn.onClick.RemoveAllListeners();
        inputField.onValueChanged.RemoveAllListeners();

        button.onClick.AddListener(CloseClickListener);
        closeBtn.onClick.AddListener(CloseClickListener);

        inputField.text = "1";

        insBtn.onClick.AddListener(() => InscreseClickListener(item));
        desBtn.onClick.AddListener(DescreseClickListener);
        inputField.onValueChanged.AddListener((text) => CheckMinMaxItemListener(text, item));

        if (item.Category == ItemCategory.Exp || item.Category == ItemCategory.Equipment)
        {
            hint.text = "Level:";
        }

        if (item.Category == ItemCategory.PrimarionSoul)
        {
            hint.text = "Primarion Souls:";
        }

        if (item.Category == ItemCategory.Item)
        {
            hint.text = "Primarion Souls:";
        }

        if (item.Category == ItemCategory.Exp || item.Category == ItemCategory.PrimarionSoul || item.Category == ItemCategory.Item)
        {
            buyBtn.gameObject.SetActive(true);
            buyBtn.onClick.RemoveAllListeners();

            maxBtn.gameObject.SetActive(false);
            randomBtn.gameObject.SetActive(false);

            quantity.text = $"{item.Cost}";

            if (item.Category == ItemCategory.Exp)
            {
                buyBtn.onClick.AddListener(BuyLevelClickListener);
            }

            if (item.Category == ItemCategory.PrimarionSoul)
            {
                buyBtn.onClick.AddListener(BuyPrimarionSoulListener);
            }
        }
        else
        {
            buyBtn.gameObject.SetActive(false);
            maxBtn.gameObject.SetActive(true);
            randomBtn.gameObject.SetActive(true);
            maxBtn.onClick.RemoveAllListeners();
            randomBtn.onClick.RemoveAllListeners();

            maxBtn.onClick.AddListener(() => BuyMaxStatListener(item));
            randomBtn.onClick.AddListener(() => BuyRandomStatListener(item));
        }
    }

    private void CloseClickListener()
    {
        this.gameObject.SetActive(false);
    }

    private void DescreseClickListener()
    {
        int number = int.Parse(inputField.text);
        number = number <= 1 ? 1 : --number;
        inputField.text = $"{number}";
    }

    private void InscreseClickListener(TrailShopItem item)
    {
        int number = int.Parse(inputField.text);
        number = number > item.MaxItem ? item.MaxItem : ++number;
        inputField.text = $"{number}";
    }

    private void CheckMinMaxItemListener(string text, TrailShopItem item)
    {
        int number = text == "" ? 1 : int.Parse(text);
        number = number < 1 ? 1 : number > item.MaxItem ? item.MaxItem : number;
        inputField.text = $"{number}";
    }

    private void BuyLevelClickListener()
    {
        if (inventory.PrimarionSoul < int.Parse(quantity.text)) return;

        player.StatsSystem.IncreaseLevel(int.Parse(inputField.text));
        inventory.DescreasePrimarionSoul(int.Parse(quantity.text));

        this.gameObject.SetActive(false);
    }

    private void BuyPrimarionSoulListener()
    {
        if (inventory.PrimarionSoul < int.Parse(quantity.text)) return;

        inventory.IncreasePrimarionSoul(int.Parse(inputField.text));
        inventory.DescreasePrimarionSoul(int.Parse(quantity.text));

        this.gameObject.SetActive(false);
    }

    private void BuyRandomStatListener(TrailShopItem item)
    {
        if (inventory.PrimarionSoul < int.Parse(quantity.text)) return;

        EquipSO equipSO = item.BaseItemSO as EquipSO;

        InventoryEquip equip = inventory.EquipObtainer.CreateEquip(equipSO, item.Quality);
        inventory.AddEquip(equip);
        inventory.EquipObtainer.UpgradeEquip(equip, int.Parse(inputField.text));
        inventory.DescreasePrimarionSoul(item.Cost);

        this.gameObject.SetActive(false);
    }

    private void BuyMaxStatListener(TrailShopItem item)
    {
        if (inventory.PrimarionSoul < int.Parse(quantity.text)) return;

        EquipSO equipSO = item.BaseItemSO as EquipSO;

        InventoryEquip equip = inventory.EquipObtainer.CreateEquip(equipSO, item.Quality, true);
        inventory.AddEquip(equip);
        inventory.EquipObtainer.UpgradeEquip(equip, int.Parse(inputField.text), true);
        inventory.DescreasePrimarionSoul(item.Cost);

        this.gameObject.SetActive(false);
    }
}