using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class MonsterSpawner : Spawner
{
    protected AttackType[] attackTypes = { AttackType.Attack, AttackType.MagicAttack, AttackType.AllAttack };

    [Header("Spawn Settings")]
    [SerializeField] private Transform map;
    [SerializeField] protected float timer = 300;
    [SerializeField] protected float counter = 0;
    [SerializeField] protected bool canSpawn = true;
    [SerializeField] protected bool lockSpawn = false;

    [Header("Strong Monster Settings")]
    [SerializeField] protected int maxStrongMonster = 4, currentStrongMonster;

    #region Properties
    public float Timer => timer;
    public float Counter => counter;
    public bool CanSpawn => canSpawn;
    public bool LockSpawn => lockSpawn;
    #endregion

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
        MonsterTier tier;

        while (spawnCount < maxSpawnCount)
        {
            if (currentStrongMonster < maxStrongMonster)
            {
                tier = GetRandomMonsterTier();
            }
            else
            {
                tier = MonsterTier.Normal;
            }

            Vector3 newPos = new Vector3(Random.Range(map.position.x - 230, map.position.x + 230), -1.75f, 0);
            Transform monster = Spawn(prefabs[Random.Range(0, prefabs.Count)], newPos, Quaternion.identity);
            IMMonster monsterCom = monster.GetComponent<IMMonster>();
            foreach (var spriteSkin in monsterCom.RigModel.GetComponentsInChildren<SpriteSkin>())
            {
                spriteSkin.enabled = true;
            }

            monsterCom.Stats.ZeroLevel = Random.Range(1, 10);
            monsterCom.Stats.AttackType = attackTypes[Random.Range(0, attackTypes.Length)];
            monsterCom.Stats.Tier = tier;

            if (tier == MonsterTier.Elite) monsterCom.RigModel.localScale = new Vector3(-0.3f, 0.3f, 0.3f);
            if (tier == MonsterTier.Rampage) monsterCom.RigModel.localScale = new Vector3(-0.4f, 0.4f, 0.4f);

            monster.gameObject.SetActive(true);

            spawnCount++;
            if (monsterCom.Stats.Tier == MonsterTier.Elite || monsterCom.Stats.Tier == MonsterTier.Rampage) currentStrongMonster++;
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
        StartCoroutine(DespawnAllMonsterCoroutine());
    }

    private IEnumerator DespawnAllMonsterCoroutine()
    {
        List<IMMonster> objs = holder.GetComponentsInChildren<IMMonster>().ToList();

        foreach (IMMonster obj in objs)
        {
            if (objPool.Contains(obj.transform)) continue;
            objPool.Add(obj.transform);
            foreach (var spriteSkin in obj.RigModel.GetComponentsInChildren<SpriteSkin>())
            {
                spriteSkin.enabled = false;
            }

            yield return null;
            yield return null;

            obj.gameObject.SetActive(false);
            
            if (obj.Stats.Tier == MonsterTier.Elite || obj.Stats.Tier == MonsterTier.Rampage) currentStrongMonster--;
        }
    }

    public MonsterTier GetRandomMonsterTier()
    {
        MonsterTier tier = MonsterTier.Normal;

        if (Random.value < 0.5f) // 50% xác suất lên Elite
        {
            tier = MonsterTier.Elite;

            if (Random.value < 0.25f) // 25% xác suất từ Elite lên Rampage
            {
                tier = MonsterTier.Rampage;
            }
        }

        return tier;
    }

    public override void Despawn(Transform obj)
    {
        base.Despawn(obj);
        IMMonster monsterCom = obj.GetComponent<IMMonster>();

        if (monsterCom.Stats.Tier == MonsterTier.Elite || monsterCom.Stats.Tier == MonsterTier.Rampage) currentStrongMonster--;
    }
}