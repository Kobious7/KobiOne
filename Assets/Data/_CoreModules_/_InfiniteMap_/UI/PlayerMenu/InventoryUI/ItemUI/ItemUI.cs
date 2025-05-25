using System;
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

    [SerializeField] private Transform onSelectObject;
    [SerializeField] private float lastTimeClick, doubleClickTheshold = 0.4f;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadModel();
        LoadQuantityText();
        LoadButton();
        LoadOnSelectObject();
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

    private void LoadOnSelectObject()
    {
        if(onSelectObject != null) return;

        onSelectObject = transform.Find("OnSelect");
    }

    public void ShowItem(InventoryItem item)
    {
        model.sprite = item.ItemSO.Sprite;
        quantityText.text = item.Quantity > 1 ? $"{item.Quantity}" : "";
    }

    private void Click()
    {
        if(!onSelectObject.gameObject.activeSelf)
        {
            if(Time.time - lastTimeClick < doubleClickTheshold)
            {
                ItemDetailsUI.Instance.gameObject.SetActive(true);
                ItemDetailsUI.Instance.ShowItem(Game.Instance.Inventory.ListItems[index]);
            }

            lastTimeClick = Time.time;

            return;
        }

        if(ItemUISpawner.Instance.SelectedItem != null) ItemUISpawner.Instance.SelectedItem.gameObject.SetActive(true);
        onSelectObject.gameObject.SetActive(false);

        ItemUISpawner.Instance.SelectedItem = onSelectObject;
    }
}