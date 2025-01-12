using UnityEngine;

namespace InfiniteMap
{
    public class PlayerShooting : PlayerAb
    {
        [SerializeField] private Transform prefab;

        public void Shoot()
        {
            Transform obj = Game.Instance.FlyObjectSpawner.Spawn(prefab, Player.AttackPoint.position, Player.CenterPoint.rotation);

            obj.gameObject.SetActive(true);
        }
    }
}