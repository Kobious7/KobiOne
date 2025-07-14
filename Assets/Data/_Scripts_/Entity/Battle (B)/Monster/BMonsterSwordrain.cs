using UnityEngine;

public class BMonsterSwordrain : BEntitySwordrain
{
    protected override Transform GetRandomSpawnPoint()
    {
        return BattleManager.Instance.MonsterSpawnPointContainer.GetRandomSpawnPoint();
    }
}