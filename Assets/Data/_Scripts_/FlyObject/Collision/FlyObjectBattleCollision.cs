using UnityEngine;

public class FlyObjecyBattleCollision : FlyObjectCollision
{
    private BattleManager battleManager;
    private BMonsterAnim monsterAnim;
    private BPlayerAnim playerAnim;

    protected override void OnEnable()
    {
        base.OnEnable();

        battleManager = BattleManager.Instance;
        playerAnim = battleManager.Player.Anim as BPlayerAnim;
        monsterAnim = battleManager.Monster.Anim as BMonsterAnim;
    }

    protected override void Collide(Collider other)
    {
        base.Collide(other);

        //battleManager.FlyObjectSpawner.Despawn(transform.parent);
        
        Debug.Log("" + other.gameObject.name);

        if (other.transform.name == "Monster")
        {
            monsterAnim.BeingHit();
            Battle.Instance.DealTileDamage(BattleManager.Instance.Player.Stats, BattleManager.Instance.Monster.Stats);
            battleManager.FlyObjectSpawner.Despawn(transform.parent);
        }

        if (other.transform.name == "Player")
        {
            playerAnim.BeingHit();
            Battle.Instance.DealTileDamage(BattleManager.Instance.Monster.Stats, BattleManager.Instance.Player.Stats);
            battleManager.FlyObjectSpawner.Despawn(transform.parent);
        }
    }
}