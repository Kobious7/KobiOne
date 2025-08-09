using UnityEngine;

public class SwordrainCollision : GMono
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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.transform.name);
        if (other.transform.name == "Player")
        {
            if (player.Stats.CurrentHP > 0)
                playerAnim.BeingHit();
                
            Battle.Instance.DealSwordrainDamage(battleManager.Monster.Stats, battleManager.Player.Stats);
            battleManager.SwordrainSpawner.Despawn(transform.parent);
        }

        if (other.transform.name == "Monster")
        {
            if (monster.Stats.CurrentHP > 0)
                monsterAnim.BeingHit();

            Battle.Instance.DealSwordrainDamage(battleManager.Player.Stats, battleManager.Monster.Stats);
            battleManager.SwordrainSpawner.Despawn(transform.parent);
        }
    }
}