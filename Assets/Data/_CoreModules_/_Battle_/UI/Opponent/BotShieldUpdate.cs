using UnityEngine;

public class BotShieldUpdate : ShieldUpdate
{
    protected override string GetNewString()
    {
        return $"{Game.Instance.Opponent.Stats.ShieldStack}";
    }
}