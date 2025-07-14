using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterSpawner : Spawner
{
    protected AttackType[] attackTypes = { AttackType.Attack, AttackType.MagicAttack, AttackType.AllAttack };
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
            Vector3 newPos = new Vector3(Random.Range(map.position.x - 230, map.position.x + 230), -1.75f, 0);
            Transform monster = Spawn(prefabs[Random.Range(0, prefabs.Count)], newPos, Quaternion.identity);
            IMMonster monsterCom = monster.GetComponent<IMMonster>();
            monsterCom.Stats.ZeroLevel = Random.Range(1, 10);
            monsterCom.Stats.AttackType = attackTypes[Random.Range(0, attackTypes.Length)];

            monster.gameObject.SetActive(true);

            spawnCount++;
        }
    }

    public List<Transform> GetActiveMonsters()
    {
        List<IMMonster> objs = holder.GetComponentsInChildren<IMMonster>().ToList();
        List<Transform> activeMonsters = new();

        foreach (IMMonster obj in objs)
        {
            if (obj.gameObject.activeSelf) activeMonsters.Add(obj.transform);
        }

        return activeMonsters;
    }

    public void DespawnAllMonster()
    {
        List<IMMonster> objs = holder.GetComponentsInChildren<IMMonster>().ToList();

        foreach (IMMonster obj in objs)
        {
            if (objPool.Contains(obj.transform)) continue;
            objPool.Add(obj.transform);
            obj.gameObject.SetActive(false);
        }
    }
}