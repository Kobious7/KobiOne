using UnityEngine;

public class BotShieldUpdate : ShieldUpdate
{
    protected override string GetNewString()
    {
        return $"{BattleManager.Instance.Monster.Stats.ShieldStack}";
    }
}