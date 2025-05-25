using UnityEngine;

public class BotHPUpdate : HPUpdate
{
    protected override float GetNewFillAmount()
    {
        return (float)Game.Instance.Opponent.Stats.CurrentHP / Game.Instance.Opponent.Stats.MaxHP;
    }

    protected override string GetNewString()
    {
        return $"{Game.Instance.Opponent.Stats.CurrentHP}/{Game.Instance.Opponent.Stats.MaxHP}";
    }
}