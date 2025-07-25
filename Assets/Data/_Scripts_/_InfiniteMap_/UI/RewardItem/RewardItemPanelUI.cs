using UnityEngine;

public class RewardItemPanelUI : GMono
{
    protected override void Start()
    {
        if (InfiniteMapManager.Instance.MapData.MapCanLoad)
        {
            this.gameObject.SetActive(true);
            
            InfiniteMapManager.Instance.IsUIOpening = true;
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}