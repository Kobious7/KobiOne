using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RequireSoulUI : GMono
{
    [SerializeField] private TextMeshProUGUI ownedSoul;
    [SerializeField] private TextMeshProUGUI requireSoul;
    [SerializeField] private TextMeshProUGUI slash;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        ownedSoul = transform.Find("OwnedSoul").GetComponent<TextMeshProUGUI>();
        requireSoul = transform.Find("RequireSoul").GetComponent<TextMeshProUGUI>();
        slash = transform.Find("Slash").GetComponent<TextMeshProUGUI>();
    }

    public void SetRequireSoul(Inventory inventory, InventoryEquip equip)
    {
        if (inventory.PrimarionSoul >= equip.CurrentUpgradeCost)
        {
            ownedSoul.color = Color.green;
        }
        else
        {
            ownedSoul.color = Color.red;
        }

        ownedSoul.text = $"{inventory.PrimarionSoul}";
        requireSoul.text = $"{equip.CurrentUpgradeCost}";

        LayoutRebuilder.ForceRebuildLayoutImmediate(ownedSoul.GetComponent<RectTransform>());
        LayoutRebuilder.ForceRebuildLayoutImmediate(requireSoul.GetComponent<RectTransform>());
    }
}