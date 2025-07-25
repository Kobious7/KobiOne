using UnityEngine;

public class PotentialPointResetUI : ResetPromptUI
{
    protected override void ClickToResetListener()
    {
        InfiniteMapManager.Instance.Player.StatsSystem.ResetPotentialPoint();
        this.transform.gameObject.SetActive(false);
    }
}