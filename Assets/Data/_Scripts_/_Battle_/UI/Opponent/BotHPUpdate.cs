using UnityEngine;

public class BotHPUpdate : HPUpdate
{
    protected override float GetNewFillAmount()
    {
        return (float)BattleManager.Instance.Monster.Stats.CurrentHP / BattleManager.Instance.Monster.Stats.MaxHP;
    }

    protected override string GetNewString()
    {
        return $"{BattleManager.Instance.Monster.Stats.CurrentHP}/{BattleManager.Instance.Monster.Stats.MaxHP}";
    }
}