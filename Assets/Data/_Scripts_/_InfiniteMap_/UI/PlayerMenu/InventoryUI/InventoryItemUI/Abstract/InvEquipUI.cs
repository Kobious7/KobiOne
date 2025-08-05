using UnityEngine;

public abstract class InvEquipUI : InventoryItemUI
{
    public override void ShowIventoryItem(InventoryItemUI inventoryItem)
    {
        InventoryEquip equip = (InventoryEquip)inventoryItem.InventoryItem;
        this.inventoryItem = equip;
        quality.color = GetQualityColorByRarity(equip.Rarity);
        image.sprite = equip.EquipSO.Sprite;

        if (equip.IsLock) lockIcon.gameObject.SetActive(true);
        else lockIcon.gameObject.SetActive(false);

        if (equip.IsNew) newIcon.gameObject.SetActive(true);
        else newIcon.gameObject.SetActive(false);

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => SeeDetailsClickListener(inventoryItem));
    }

    public void ReshowNewLock()
    {
        InventoryEquip equip = (InventoryEquip)inventoryItem;
        
        if (equip.IsLock) lockIcon.gameObject.SetActive(true);
        else lockIcon.gameObject.SetActive(false);

        if (equip.IsNew) newIcon.gameObject.SetActive(true);
        else newIcon.gameObject.SetActive(false);
    }

    public void ReverseLockAndShow()
    {
        InventoryEquip equip = (InventoryEquip)inventoryItem;
        equip.IsLock = !equip.IsLock;

        if (equip.IsLock) lockIcon.gameObject.SetActive(true);
        else lockIcon.gameObject.SetActive(false);
    }

    protected override void OffSelected()
    {
        newIcon.gameObject.SetActive(false);
        InventoryEquip equip = (InventoryEquip)inventoryItem;
        if (equip.IsNew)
        {
            equip.IsNew = false;
            NewAndLockEquip.Instance.OnNewOrLockChangedEventInvoke(equip, false);
        }

        OffSelectedWithSecificEquip();
    }

    protected override void OnDetails(InventoryItemUI inventoryItem)
    {
        InventoryEquip equip = (InventoryEquip)inventoryItem.InventoryItem;

        InvEquipDetailsUI.Instance.gameObject.SetActive(true);
        InvEquipDetailsUI.Instance.ShowDetails(inventoryItem);
    }

    protected abstract void OffSelectedWithSecificEquip();
}