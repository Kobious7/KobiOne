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
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadModel();
        LoadQuantityText();
    }

    private void LoadModel()
    {
        if(model != null) return;

        model = transform.Find("Item").GetComponent<Image>();
    }

    private void LoadQuantityText()
    {
        if(quantityText != null) return;

        quantityText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ShowItem(InventoryItem item)
    {
        model.sprite = item.ItemSO.Sprite;
        quantityText.text = item.Quantity > 1 ? $"{item.Quantity}/{item.ItemSO.MaxStack}" : "1";
    }
}