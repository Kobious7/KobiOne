using UnityEngine;

public class SwordrainCollision : GMono
{
    private BattleManager battleManager;
    private BMonsterAnim monsterAnim;
    private BPlayerAnim playerAnim;

    protected override void OnEnable()
    {
        base.OnEnable();

        battleManager = BattleManager.Instance;
        monsterAnim = battleManager.Monster.Anim as BMonsterAnim;
        playerAnim = battleManager.Player.Anim as BPlayerAnim;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.transform.name);
        if (other.transform.name == "Player")
        {
            playerAnim.BeingHit();
            Battle.Instance.DealSwordrainDamage(battleManager.Monster.Stats, battleManager.Player.Stats);
            battleManager.SwordrainSpawner.Despawn(transform.parent);
        }

        if (other.transform.name == "Monster")
        {
            monsterAnim.BeingHit();
            Battle.Instance.DealSwordrainDamage(battleManager.Player.Stats, battleManager.Monster.Stats);
            battleManager.SwordrainSpawner.Despawn(transform.parent);
        }
    }
}