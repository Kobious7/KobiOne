using UnityEngine;

public class PlayerShieldUpdate : ShieldUpdate
{
    protected override string GetNewString()
    {
        return $"{BattleManager.Instance.Player.Stats.ShieldStack}";
    }
}