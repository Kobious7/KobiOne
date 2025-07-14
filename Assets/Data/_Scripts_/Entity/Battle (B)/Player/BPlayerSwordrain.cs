using UnityEngine;

public class BPlayerSwordrain : BEntitySwordrain
{
    protected override Transform GetRandomSpawnPoint()
    {
        return BattleManager.Instance.PlayerSpawnPointContainer.GetRandomSpawnPoint();
    }
}