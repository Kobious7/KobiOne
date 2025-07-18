using System;
using UnityEngine;

public class EntityAttackPoint : EntityComponent
{
    private void Update()
    {
        LookTarget();
    }

    private void LookTarget()
    {
        Vector3 targetPos = to2DVec(GetTargetPos());
        Vector3 direction = targetPos - Entity.CenterPoint.position;
        //transform.position = Player.CenterPoint.position + direction.normalized * distance;
        float rotZ = (float)(Math.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        Entity.CenterPoint.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    protected virtual Vector3 GetTargetPos()
    {
        return Vector3.zero;
    }
}