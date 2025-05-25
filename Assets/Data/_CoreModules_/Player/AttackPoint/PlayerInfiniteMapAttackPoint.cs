using UnityEngine;

public class PlayerInfiniteMapAttackPoint : PlayerAttackPoint
{
    protected override Vector3 GetTargetPos()
    {
        return InputManager.Instance.MousePos;
    }
}