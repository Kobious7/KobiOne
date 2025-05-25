using System.Collections;
using UnityEngine;

public class PlayerBattleRangedAttack : PlayerRangedAttack
{
    public override IEnumerator BattleRangedAttack()
    {
        Player.Anim.RangedAttack();

        yield return StartCoroutine(Player.Anim.WaitAnim("SwordRangedAttack"));

        while (Game.Instance.FlyObjectSpawner.ActiveCount > 0)
        {
            yield return null;
        }

        Player.Anim.Idle();

        yield return StartCoroutine(Player.Anim.WaitAnim("Idle"));
    }
}