using UnityEngine;

public class BPlayerAttackPoint : EntityAttackPoint
{
    protected override Vector3 GetTargetPos()
    {
        return BattleManager.Instance.Monster.CenterPoint.position;
    }
}