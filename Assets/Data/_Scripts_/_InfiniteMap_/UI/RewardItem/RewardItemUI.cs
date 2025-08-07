using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardItemUI : GMono
{
    [SerializeField] private Image image, quality;
    [SerializeField] private TextMeshProUGUI quantity;
    private IMPlayerStats playerStats;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        image = transform.Find("Image").GetComponent<Image>();
        quality = transform.Find("Quality").GetComponent<Image>();
        quantity = transform.Find("Quantity").GetComponent<TextMeshProUGUI>();
    }

    public void SetExpRewardItemUI(int exp)
    {
        quantity.text = $"{exp}";
    }

    public void SetPrimaSoulRewardItemUI(int primarionSoul)
    {
        quantity.text = $"{primarionSoul}";
    }

    public void SetRewardItemUI(InventoryStuff item)
    {
        if (item is InventoryItem inventoryItem)
        {
            image.sprite = inventoryItem.ItemSO.Sprite;
            quality.color = GetQualityColorByRarity(inventoryItem.ItemSO.Rarity);
            quantity.text = inventoryItem.Quantity > 1 ? $"{inventoryItem.Quantity}" : "";
        }

        if (item is InventoryEquip inventoryEquip)
        {
            image.sprite = inventoryEquip.EquipSO.Sprite;
            quality.color = GetQualityColorByRarity(inventoryEquip.Rarity);
            quantity.text = "";
        }
    }
}