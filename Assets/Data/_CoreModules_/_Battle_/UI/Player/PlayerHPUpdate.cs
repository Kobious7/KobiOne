using UnityEngine;

public class PlayerHPUpdate : HPUpdate
{
    protected override float GetNewFillAmount()
    {
        return (float)BattleManager.Instance.Player.Stats.CurrentHP / BattleManager.Instance.Player.Stats.MaxHP;
    }

    protected override string GetNewString()
    {
        return $"{BattleManager.Instance.Player.Stats.CurrentHP}/{BattleManager.Instance.Player.Stats.MaxHP}";
    }
}