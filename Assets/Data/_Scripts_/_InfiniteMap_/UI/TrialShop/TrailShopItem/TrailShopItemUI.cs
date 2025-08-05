using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrailShopItemUI : GMono
{
    [SerializeField] private Image image, quality;
    [SerializeField] private Button buyBtn;
    [SerializeField] private TextMeshProUGUI cost;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        image = transform.Find("Image").GetComponent<Image>();
        quality = transform.Find("Quality").GetComponent<Image>();
        buyBtn = transform.Find("BuyBtn").GetComponent<Button>();
        cost = buyBtn.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetTrailShopItemUI(TrailShopItem item)
    {
        if (item.Category == ItemCategory.Item || item.Category == ItemCategory.Equipment) image.sprite = item.BaseItemSO.Sprite;
        quality.color = GetQualityColorByRarity(item.Quality);
        cost.text = $"{item.Cost}";
        buyBtn.onClick.AddListener(() => BuyClickListener(item));
    }

    private void BuyClickListener(TrailShopItem item)
    {
        TrailShopItemDetailsUI.Instance.gameObject.SetActive(true);
        TrailShopItemDetailsUI.Instance.SetTrailShopItemDetails(item);
    }
}