using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace InfiniteMap
{
    public class FlyObjectCollison : FlyObjectAb
    {
        private void OnTriggerEnter(Collider other)
        {
            Game.Instance.FlyObjectSpawner.Despawn(transform.parent);

            if (other.transform.name == "Monster")
            {
                Game.Instance.LoadAllObj(other.transform);
                PlayerStatic.Instance.SetGameObject(Game.Instance.Player.RigModel);
                PlayerStatic.Instance.LoadBattle("Battle");
            }
        }
    }
}