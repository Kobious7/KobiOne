using UnityEngine;
using UnityEngine.UI;

public class TrailShopBtn : GMono
{
    [SerializeField] private Button button;
    [SerializeField] private Transform trialShop;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        if (button == null) button = GetComponent<Button>();
        if (trialShop == null) trialShop = transform.Find("CanvasGroup").Find("TrailShop");
    }

    protected override void Start()
    {
        base.Start();
        button.onClick.AddListener(OpenTrailShopClickListener);
    }

    private void OpenTrailShopClickListener()
    {
        trialShop.gameObject.SetActive(true);
        InfiniteMapManager.Instance.IsUIOpening = true;
        button.interactable = false;
    }
}