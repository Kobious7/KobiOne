using UnityEngine;

public class BMonsterAttackPoint : EntityAttackPoint
{
    protected override Vector3 GetTargetPos()
    {
        return BattleManager.Instance.Player.CenterPoint.position;
    }
}