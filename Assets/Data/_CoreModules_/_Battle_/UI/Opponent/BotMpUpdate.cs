using UnityEngine;

public class BotMPUpdate : MPUpdate
{
    protected override float GetNewFillAmount()
    {
        return (float)Game.Instance.Opponent.Stats.Mana / 100;
    }

    protected override string GetNewString()
    {
        return $"{Game.Instance.Opponent.Stats.Mana}%";
    }
}