using UnityEngine;

public class PlayerSwrodrainFlying : SwrodrainFlying
{
    protected override void GetTarget()
    {
        target = BattleManager.Instance.Monster.transform;
    }
}