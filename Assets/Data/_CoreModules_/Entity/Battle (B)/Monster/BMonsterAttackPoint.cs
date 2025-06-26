using UnityEngine;

public class BMonsterAttackPoint : BEntityAttackPoint
{
    protected override Vector3 GetTargetPos()
    {
        return BattleManager.Instance.Player.CenterPoint.position;
    }
}