using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InfiniteMap
{
    public class MonsterSpawner : Spawner
    {
        [SerializeField] private Transform map;
        [SerializeField] protected float timer = 300;

        public float Timer => timer;

        [SerializeField] protected float counter = 0;

        public float Counter => counter;

        [SerializeField] protected bool canSpawn = true;

        public bool CanSpawn => canSpawn;

        [SerializeField] protected bool lockSpawn = false;

        public bool LockSpawn => lockSpawn;

        protected void Update()
        {
            if (lockSpawn) return;

            Spawn();
        }

        private void Spawn()
        {
            CountTime();

            if (!canSpawn) return;

            SpawnMonster();
        }

        private void CountTime()
        {
            if (counter >= timer)
            {
                canSpawn = true;
                return;
            }

            counter += Time.deltaTime;
        }

        private void SpawnMonster()
        {
            canSpawn = false;
            counter = 0;

            while (spawnCount < maxSpawnCount)
            {
                Vector3 newPos = new Vector3(Random.Range(map.position.x - 245, map.position.x + 245), -1.75f, 0);
                Transform monster = Spawn(prefabs[Random.Range(0, prefabs.Count)], newPos, Quaternion.identity);

                monster.gameObject.SetActive(true);

                spawnCount++;
            }
        }

        public List<Transform> GetActiveMonster()
        {
            List<Monster> objs = holder.GetComponentsInChildren<Monster>().ToList();
            List<Transform> activeMonsters = new();

            foreach (Monster obj in objs)
            {
                if (obj.gameObject.activeSelf) activeMonsters.Add(obj.transform);
            }

            return activeMonsters;
        }

        public void DespawnAllMonster()
        {
            List<Monster> objs = holder.GetComponentsInChildren<Monster>().ToList();

            foreach (Monster obj in objs)
            {
                if (objPool.Contains(obj.transform)) continue;
                objPool.Add(obj.transform);
                obj.gameObject.SetActive(false);
            }
        }
    }
}