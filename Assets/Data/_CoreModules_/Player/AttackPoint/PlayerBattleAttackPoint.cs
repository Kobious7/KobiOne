using UnityEngine;

public class PlayerBattleAttackPoint : PlayerAttackPoint
{
    protected override Vector3 GetTargetPos()
    {
        return Game.Instance.Opponent.transform.position;
    }
}