using UnityEngine;

public class PlayerShieldUpdate : ShieldUpdate
{
    protected override string GetNewString()
    {
        return $"{Game.Instance.Player.BattleStats.ShieldStack}";
    }
}