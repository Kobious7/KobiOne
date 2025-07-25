using UnityEngine;

public class BPlayerSpriteSwap : PlayerSpriteSwap
{
    protected override void InitSet()
    {
        playerData = BattleManager.Instance.PlayerData;
        
        base.InitSet();
    }
}