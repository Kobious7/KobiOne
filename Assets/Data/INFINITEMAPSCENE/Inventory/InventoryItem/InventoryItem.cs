using System;
using UnityEngine;

[Serializable]
public class InventoryItem
{
    [SerializeField] private ItemSO itemSO;

    public ItemSO ItemSO
    {
        get => itemSO;
        set => itemSO = value;
    }

    [SerializeField] private int quantity;

    public int Quantity
    {
        get => quantity;
        set => quantity = value;
    }

    public InventoryItem(ItemSO itemSO, int quantity)
    {
        this.itemSO = itemSO;
        this.quantity = quantity;
    }
}