using UnityEngine;

public class MonsterSwordrainFlying : SwrodrainFlying
{
    protected override void GetTarget()
    {
        target = BattleManager.Instance.Player.transform;
    }
}