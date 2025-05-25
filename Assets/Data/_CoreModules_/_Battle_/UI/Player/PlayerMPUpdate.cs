using UnityEngine;

public class PlayerMPUpdate : MPUpdate
{
    protected override float GetNewFillAmount()
    {
        return (float)Game.Instance.Player.BattleStats.Mana / 100;
    }

    protected override string GetNewString()
    {
        return $"{Game.Instance.Player.BattleStats.Mana}%";
    }
}