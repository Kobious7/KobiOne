using UnityEngine;

public class FlyObjecyBattleCollision : FlyObjectCollision
{
    protected override void Collide(Collider other)
    {
        base.Collide(other);

        Game.Instance.FlyObjectSpawner.Despawn(transform.parent);

        if (other.transform.name == "Opponent")
        {
            Battle.Instance.DealFlyObjectDamage(Game.Instance.Player.BattleStats, Game.Instance.Opponent.Stats);
            Battle.Instance.PlayerNextDamage = DamageType.SwordrainDamage;
            Game.Instance.FlyObjectSpawner.Despawn(transform.parent);
        }
    }
}