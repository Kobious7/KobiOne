using UnityEngine;

public class SwordrainCollision : GMono
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.transform.name);
        if (other.transform.name == "Player")
        {
            Battle.Instance.DealSwordrainDamage(Game.Instance.Opponent.Stats, Game.Instance.Player.BattleStats);
            Game.Instance.SwordrainSpawner.Despawn(transform.parent);
        }
        
        if (other.transform.name == "Opponent")
        {
            Battle.Instance.DealSwordrainDamage(Game.Instance.Player.BattleStats, Game.Instance.Opponent.Stats);
            Battle.Instance.PlayerNextDamage = DamageType.SwordrainDamage;
            Game.Instance.SwordrainSpawner.Despawn(transform.parent);
        }
    }
}