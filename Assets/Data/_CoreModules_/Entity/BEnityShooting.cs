using UnityEngine;

public class BEntityShooting : EntityComponent, IEntityShooting
{
    [SerializeField] private Transform prefab;

    public void Shoot()
    {
        Transform obj = BattleManager.Instance.FlyObjectSpawner.Spawn(prefab, Entity.AttackPoint.transform.position, Entity.CenterPoint.rotation);

        obj.gameObject.SetActive(true);
    }
}