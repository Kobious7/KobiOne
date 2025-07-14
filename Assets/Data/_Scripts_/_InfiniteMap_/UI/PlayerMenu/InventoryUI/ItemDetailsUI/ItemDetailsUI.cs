using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailsUI : GMono
{
    private static ItemDetailsUI instance;

    public static ItemDetailsUI Instance => instance;


    [SerializeField] private Image model;

    public Image Model
    {
        get => model;
        set => model = value;
    }

    [SerializeField] private TextMeshProUGUI nameText;

    public TextMeshProUGUI NameText => nameText;

    [SerializeField] private DescriptionUITMP description;

    public DescriptionUITMP Description => description;

    [SerializeField] private TextMeshProUGUI quantityText;

    public TextMeshProUGUI QuantityText
    {
        get => quantityText;
        set => quantityText = value;
    }

    protected override void Awake()
    {
        base.Awake();
        if(instance != null) Debug.LogError("Only 1 ItemDetailsUI is allowed to exist!");

        instance = this;
        instance.gameObject.SetActive(false);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadModel();
        LoadNameText();
        LoadDescription();
        LoadQuantityText();
    }

    private void LoadModel()
    {
        if(model != null) return;

        model = transform.Find("Details").Find("Model").GetComponent<Image>();
    }

    private void LoadDescription()
    {
        if(description != null) return;

        description = transform.Find("Details").GetComponentInChildren<DescriptionUITMP>();
    }

    private void LoadNameText()
    {
        if(nameText != null) return;

        nameText = transform.Find("Details").Find("Name").GetComponent<TextMeshProUGUI>();
    }

    private void LoadQuantityText()
    {
        if(quantityText != null) return;

        quantityText = transform.Find("Details").Find("Quantity").GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ShowItem(InventoryItem item)
    {
        model.sprite = item.ItemSO.Sprite;
        nameText.text = item.ItemSO.ItemName;
        description.Description.text = item.Quantity + "";
        quantityText.text = item.Quantity > 1 ? $"{item.Quantity}/{item.ItemSO.MaxStack}" : $"1/{item.ItemSO.MaxStack}";
    }
}