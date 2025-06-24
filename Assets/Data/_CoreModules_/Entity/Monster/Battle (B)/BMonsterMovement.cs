using UnityEngine;

public class BMonsterMovement : BEntityMovement
{
    protected override void Start()
    {
        base.Start();
        targetPos = BattleManager.Instance.Player.transform;
    }

    protected override void GetTargetRadius()
    {
        targetRadius = BattleManager.Instance.Player.CapsuleCollider.radius;
    }
}