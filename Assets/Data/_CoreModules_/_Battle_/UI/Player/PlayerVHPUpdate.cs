using UnityEngine;

public class PlayerVHPUpdate : VHPUpdate
{
    protected override float GetNewFillAmount()
    {
        return (float)Game.Instance.Player.BattleStats.VHP / Game.Instance.Player.BattleStats.MaxHP * 2;
    }
}