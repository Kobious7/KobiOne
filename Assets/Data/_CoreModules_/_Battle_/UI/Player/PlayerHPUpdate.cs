using UnityEngine;

public class PlayerHPUpdate : HPUpdate
{
    protected override float GetNewFillAmount()
    {
        return (float)Game.Instance.Player.BattleStats.CurrentHP / Game.Instance.Player.BattleStats.MaxHP;
    }

    protected override string GetNewString()
    {
        return $"{Game.Instance.Player.BattleStats.CurrentHP}/{Game.Instance.Player.BattleStats.MaxHP}";
    }
}