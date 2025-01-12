using System;
using InfiniteMap;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : GMono
{
    [SerializeField] private int index;

    public int Index
    {
        get => index;
        set => index = value;
    }

    [SerializeField] private ItemSO itemSO;

    public ItemSO ItemSO
    {
        get => itemSO;
        set => itemSO = value;
    }

    [SerializeField] private Image model;

    public Image Model
    {
        get => model;
        set => model = value;
    }

    [SerializeField] private Button btn;

    public Button Btn
    {
        get => btn;
        set => btn = value;
    }

    [SerializeField] private TextMeshProUGUI quantityText;

    public TextMeshProUGUI QuantityText
    {
        get => quantityText;
        set => quantityText = value;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadModel();
        LoadQuantityText();
        LoadButton();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        btn.onClick.AddListener(Click);
    }

    private void LoadModel()
    {
        if(model != null) return;

        model = transform.Find("Image").GetComponent<Image>();
    }

    private void LoadQuantityText()
    {
        if(quantityText != null) return;

        quantityText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void LoadButton()
    {
        if(btn != null) return;

        btn = GetComponentInChildren<Button>();
    }

    public void ShowItem(InventoryItem item)
    {
        model.sprite = item.ItemSO.Sprite;
        quantityText.text = item.Quantity > 1 ? $"{item.Quantity}" : "";
    }

    private void Click()
    {
        ItemDetailsUI.Instance.ShowItem(Game.Instance.Inventory.ListItems[this.Index]);
        GameUI.Instance.BtnsUI.UseBtnAddListener(Game.Instance.Inventory.ListItems[this.Index]);
        GameUI.Instance.BtnsUI.RemoveBtnAddListener(Game.Instance.Inventory.ListItems[this.Index]);
    }
}