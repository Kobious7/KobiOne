using UnityEngine;

public class PlayerVHPUpdate : VHPUpdate
{
    protected override float GetNewFillAmount()
    {
        return (float)BattleManager.Instance.Player.Stats.VHP / BattleManager.Instance.Player.Stats.MaxHP * 2;
    }
}