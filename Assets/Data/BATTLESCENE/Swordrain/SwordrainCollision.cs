using UnityEngine;

namespace Battle
{
    public class SwordrainCollision : GMono
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.name == "Player")
            {
                Battle.Instance.DealSwordrainDamage(Game.Instance.Bot, Game.Instance.Player);
                Game.Instance.SwordrainSpawner.Despawn(transform.parent);
            }
            
            if (other.transform.name == "Opponent")
            {
                Battle.Instance.DealSwordrainDamage(Game.Instance.Player, Game.Instance.Bot);
                Game.Instance.SwordrainSpawner.Despawn(transform.parent);
            }
        }
    }
}