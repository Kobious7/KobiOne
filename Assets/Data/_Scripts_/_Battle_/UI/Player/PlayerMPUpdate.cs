using UnityEngine;

public class PlayerMPUpdate : MPUpdate
{
    protected override float GetNewFillAmount()
    {
        return (float)BattleManager.Instance.Player.Stats.Mana / 100;
    }

    protected override string GetNewString()
    {
        return $"{BattleManager.Instance.Player.Stats.Mana}%";
    }
}