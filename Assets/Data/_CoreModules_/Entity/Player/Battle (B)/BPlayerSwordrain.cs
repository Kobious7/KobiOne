using UnityEngine;

public class BPlayerSwordrain : BEnitySwordrain
{
    protected override Transform GetRandomSpawnPoint()
    {
        return BattleManager.Instance.PlayerSpawnPointContainer.GetRandomSpawnPoint();
    }
}