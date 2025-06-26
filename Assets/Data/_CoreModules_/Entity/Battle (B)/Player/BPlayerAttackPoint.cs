using UnityEngine;

public class BPlayerAttackPoint : BEntityAttackPoint
{
    protected override Vector3 GetTargetPos()
    {
        return BattleManager.Instance.Monster.CenterPoint.position;
    }
}