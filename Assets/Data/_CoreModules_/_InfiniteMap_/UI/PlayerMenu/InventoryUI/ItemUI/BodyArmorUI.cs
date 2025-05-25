using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BodyArmorUI : GMono
{
    [SerializeField] private int index;

    public int Index
    {
        get => index;
        set => index = value;
    }

    [SerializeField] private InventoryEquip equip;

    public InventoryEquip Equip
    {
        get => equip;
        set => equip = value;
    }

    [SerializeField] private Image model;
    [SerializeField] private Button btn;
    [SerializeField] private Image qualityColor;
    [SerializeField] private Transform onSelectObject;
    [SerializeField] private float lastTimeClick, doubleClickTheshold = 0.4f;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadModel();
        LoadButton();
        LoadQualityColor();
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
    
    private void LoadButton()
    {
        if(btn != null) return;

        btn = GetComponentInChildren<Button>();
    }

    private void LoadQualityColor()
    {
        if(qualityColor != null) return;

        qualityColor = transform.Find("Outline").GetComponent<Image>();
    }

    private void LoadOnSelectObject()
    {
        if(onSelectObject != null) return;

        onSelectObject = transform.Find("OnSelect");
    }

    public void ShowWeapon(InventoryEquip item)
    {
        model.sprite = item.EquipSO.Sprite;
        qualityColor.color = GetQualityColorByRarity(item.Rarity);
    }

    private void Click()
    {
        if(!onSelectObject.gameObject.activeSelf)
        {
            if(Time.time - lastTimeClick < doubleClickTheshold)
            {
                EquipDetailsUI.Instance.gameObject.SetActive(true);
                EquipDetailsUI.Instance.ShowDetails(Game.Instance.Inventory.BodyArmorList[index]);
            }

            lastTimeClick = Time.time;

            return;
        }

        if(BodyArmorUISpawner.Instance.SelectedItem != null) BodyArmorUISpawner.Instance.SelectedItem.gameObject.SetActive(true);
        onSelectObject.gameObject.SetActive(false);

        BodyArmorUISpawner.Instance.SelectedItem = onSelectObject;
    }
}