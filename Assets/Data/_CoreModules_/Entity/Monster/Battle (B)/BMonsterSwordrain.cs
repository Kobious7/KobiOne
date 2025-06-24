using UnityEngine;

public class BMonsterSwordrain : BEnitySwordrain
{
    protected override Transform GetRandomSpawnPoint()
    {
        return BattleManager.Instance.MonsterSpawnPointContainer.GetRandomSpawnPoint();
    }
}