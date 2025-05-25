using UnityEngine;

public class BotVHPUpdate : VHPUpdate
{
    protected override float GetNewFillAmount()
    {
        return (float)Game.Instance.Opponent.Stats.VHP / Game.Instance.Opponent.Stats.MaxHP * 2;
    }
}