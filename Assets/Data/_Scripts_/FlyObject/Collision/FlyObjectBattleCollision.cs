using UnityEngine;

public class FlyObjecyBattleCollision : FlyObjectCollision
{
    private BattleManager battleManager;
    private BPlayer player;
    private BMonster monster;
    private BMonsterAnim monsterAnim;
    private BPlayerAnim playerAnim;

    protected override void OnEnable()
    {
        base.OnEnable();

        battleManager = BattleManager.Instance;
        player = battleManager.Player;
        monster = battleManager.Monster;
        playerAnim = player.Anim as BPlayerAnim;
        monsterAnim = monster.Anim as BMonsterAnim;
    }

    protected override void Collide(Collider other)
    {
        base.Collide(other);

        //battleManager.FlyObjectSpawner.Despawn(transform.parent);
        
        Debug.Log("" + other.gameObject.name);

        if (other.transform.name == "Monster")
        {
            if (monster.Stats.CurrentHP > 0)
                monsterAnim.BeingHit();

            Battle.Instance.DealTileDamage(BattleManager.Instance.Player.Stats, BattleManager.Instance.Monster.Stats);
            battleManager.FlyObjectSpawner.Despawn(transform.parent);
        }

        if (other.transform.name == "Player")
        {
            if (player.Stats.CurrentHP > 0)
                playerAnim.BeingHit();

            Battle.Instance.DealTileDamage(BattleManager.Instance.Monster.Stats, BattleManager.Instance.Player.Stats);
            battleManager.FlyObjectSpawner.Despawn(transform.parent);
        }
    }
}