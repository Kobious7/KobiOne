using UnityEngine;

public class BEntityShooting : BEntityComponent, IEntityShooting
{
    [SerializeField] private Transform prefab;

    public void Shoot()
    {
        Transform prefabFix = prefab;
        if (Entity is BMonster monster)
        {
            if (BattleManager.Instance.MapData.MonsterInfo.Tier == MonsterTier.Elite)
            {
                prefabFix.localScale = new Vector3(1.3f, 1.3f, 1.3f);
            }
            if (BattleManager.Instance.MapData.MonsterInfo.Tier == MonsterTier.Rampage)
            {
                prefabFix.localScale = new Vector3(1.4f, 1.4f, 1.4f);
            }
        }

        Transform obj = BattleManager.Instance.FlyObjectSpawner.Spawn(prefabFix, Entity.AttackPoint.transform.position, Entity.CenterPoint.rotation);

        obj.gameObject.SetActive(true);
    }
}