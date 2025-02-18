using InfiniteMap;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipDetailsUI : GMono
{
    private static EquipDetailsUI instance;

    public static EquipDetailsUI Instance => instance;


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

    protected override void Awake()
    {
        base.Awake();
        if(instance != null) Debug.LogError("Only 1 EquipDetailsUI is allowed to exist!");

        instance = this;
        instance.gameObject.SetActive(false);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadModel();
        LoadNameText();
        LoadDescription();
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

    public void ShowEquip(InventoryEquip equip)
    {
        model.sprite = equip.EquipSO.Sprite;
        nameText.text = equip.EquipSO.ItemName;
        description.Description.text = equip.EquipSO.ItemName;
    }
}