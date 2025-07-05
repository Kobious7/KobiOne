using UnityEngine;

public class IMPlayerAttackPoint : EntityAttackPoint
{
    protected override Vector3 GetTargetPos()
    {
        return InputManager.Instance.MousePos;
    }
}