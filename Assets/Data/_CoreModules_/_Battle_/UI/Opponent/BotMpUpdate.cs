using UnityEngine;

public class BotMPUpdate : MPUpdate
{
    protected override float GetNewFillAmount()
    {
        return (float)BattleManager.Instance.Monster.Stats.Mana / 100;
    }

    protected override string GetNewString()
    {
        return $"{BattleManager.Instance.Monster.Stats.Mana}%";
    }
}