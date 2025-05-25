using System.Collections;
using UnityEngine;

public class PlayerShooting : PlayerComponent
{
    [SerializeField] private Transform prefab;

    public void Shoot()
    {
        Transform obj = Game.Instance.FlyObjectSpawner.Spawn(prefab, Player.AttackPoint.transform.position, Player.CenterPoint.rotation);

        obj.gameObject.SetActive(true);
    }
}