using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IMEntityShooting : EntityComponent, IEntityShooting
{
    [SerializeField] private Transform prefab;

    public void Shoot()
    {
        Transform obj = InfiniteMapManager.Instance.FlyObjectSpawner.Spawn(prefab, Entity.AttackPoint.transform.position, Entity.CenterPoint.rotation);

        obj.gameObject.SetActive(true);
    }
}