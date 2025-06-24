using UnityEngine;

public class BotVHPUpdate : VHPUpdate
{
    protected override float GetNewFillAmount()
    {
        return (float)BattleManager.Instance.Monster.Stats.VHP / BattleManager.Instance.Monster.Stats.MaxHP * 2;
    }
}