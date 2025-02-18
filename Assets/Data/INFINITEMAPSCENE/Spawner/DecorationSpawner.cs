using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InfiniteMap
{
    public class DecorationSpawner : Spawner
    {
        [SerializeField] private int quantity;
        private Map map;

        protected override void Start()
        {
            base.Start();
            map = Game.Instance.Map;

            map.MapSwap.OnMapSwap += SpawnDecorations;
            //SpawnDecorations(MapEnum.Map0);
        }

        private void SpawnDecorations(MapEnum currentMap)
        {
            int quantityTemp = quantity;

            DespawnAllDeco();

            while(quantityTemp > 0)
            {
                Transform current = currentMap == MapEnum.Map0 ? map.Maps[0] : map.Maps[1];
                Transform randDeco = prefabs[Random.Range(0, prefabs.Count)];
                Vector3 newPos = new Vector3(Random.Range(current.position.x - 245, current.position.x + 245), randDeco.position.y, randDeco.position.z);
                Transform deco = Spawn(randDeco, newPos, Quaternion.identity);
                deco.gameObject.SetActive(true);

                quantityTemp--;
            }
        }

        private void DespawnAllDeco()
        {
            List<Transform> list = holder.GetComponentsInChildren<Transform>()
                                        .Where(item => item != holder.transform)
                                        .ToList();

            if(list.Count <= 0) return;

            foreach(Transform item in list)
            {
                Despawn(item);
            }
        }
    }
}