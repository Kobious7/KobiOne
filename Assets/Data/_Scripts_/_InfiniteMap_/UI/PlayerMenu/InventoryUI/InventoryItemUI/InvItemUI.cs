using UnityEngine;

public class InvItemUI : InventoryItemUI
{
    public override void ShowIventoryItem(InventoryItemUI inventoryItem)
    {
        InventoryItem item = (InventoryItem) inventoryItem.InventoryItem;
        this.inventoryItem = item;
        quality.color = GetQualityColorByRarity(item.ItemSO.Rarity);
        image.sprite = item.ItemSO.Sprite;

        newIcon.gameObject.SetActive(false);
        lockIcon.gameObject.SetActive(false);

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => SeeDetailsClickListener(inventoryItem));
    }

    protected override void OffSelected()
    {
        if(InvItemUISpawner.Instance.SelectedItem != null) InvItemUISpawner.Instance.SelectedItem.gameObject.SetActive(true);
        InvItemUISpawner.Instance.SelectedItem = onSelectObject;
    }

    protected override void OnDetails(InventoryItemUI inventoryItem)
    {
        InventoryItem item = (InventoryItem)inventoryItem.InventoryItem;

        InvItemDetailsUI.Instance.gameObject.SetActive(true);
        InvItemDetailsUI.Instance.ShowItem(item);
    }
}