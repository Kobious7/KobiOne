using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class InventoryItemUI : GMono
{
    [SerializeField] protected InventoryStuff inventoryItem;

    public InventoryStuff InventoryItem { get => inventoryItem; set => inventoryItem = value;}
    
    [SerializeField] protected Image quality, image, newIcon, lockIcon;
    [SerializeField] protected Button button;
    [SerializeField] protected TextMeshProUGUI quantityText;
    [SerializeField] protected Transform onSelectObject;
    [SerializeField] protected float lastTimeClick, doubleClickTheshold = 0.4f;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        quality = transform.Find("Quality").GetComponent<Image>();
        image = transform.Find("Image").GetComponent<Image>();
        quantityText = GetComponentInChildren<TextMeshProUGUI>();
        button = GetComponent<Button>();
        onSelectObject = transform.Find("OnSelect");
        newIcon = transform.Find("New").GetComponent<Image>();
        lockIcon = transform.Find("Lock").GetComponent<Image>();
    }

    public abstract void ShowIventoryItem(InventoryItemUI inventoryItem);

    protected void SeeDetailsClickListener(InventoryItemUI inventoryItem)
    {
        if (!onSelectObject.gameObject.activeSelf)
        {
            if (Time.time - lastTimeClick < doubleClickTheshold)
            {
                OnDetails(inventoryItem);
            }

            lastTimeClick = Time.time;

            return;
        }

        OffSelected();

        onSelectObject.gameObject.SetActive(false);
    }

    protected abstract void OnDetails(InventoryItemUI inventoryItem);
    protected abstract void OffSelected();
}