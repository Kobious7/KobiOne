using UnityEngine;

public class RewardItemPanelUI : GMono
{
    protected override void Start()
    {
        if (InfiniteMapManager.Instance.MapData.MapCanLoad)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}