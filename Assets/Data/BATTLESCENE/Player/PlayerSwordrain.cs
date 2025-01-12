using UnityEngine;

namespace Battle
{
    public class PlayerSwordrain : EntitySwordrain
    {
        protected override Transform GetRandomSpawnPoint()
        {
            return Game.Instance.PlayerSpawnPointContainer.GetRandomSpawnPoint();
        }
    }
}