using UnityEngine;

namespace Battle
{
    public class BotSwordrain : EntitySwordrain
    {
        protected override Transform GetRandomSpawnPoint()
        {
            return Game.Instance.BotSpawnPointContainer.GetRandomSpawnPoint();
        }
    }
}