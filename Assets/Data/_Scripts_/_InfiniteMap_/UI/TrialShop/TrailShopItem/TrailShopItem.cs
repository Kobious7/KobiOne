using System;

[Serializable]
public class TrailShopItem
{
    public BaseItemSO BaseItemSO;
    public ItemCategory Category;
    public int Cost;
    public Rarity Quality;
    public int MaxItem;

    public TrailShopItem(BaseItemSO baseItemSO, ItemCategory category, int cost, Rarity quality, int maxItem)
    {
        BaseItemSO = baseItemSO;
        Category = category;
        Cost = cost;
        Quality = quality;
        MaxItem = maxItem;
    }
}