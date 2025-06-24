using System.Collections;
using UnityEngine;

public class BEnitySwordrain : EntityComponent
{
    [SerializeField] private Transform sword;

    public Transform Sword => sword;

    private float[] waitTime = new float[] { 0, 0.2f, 0.3f, 0.5f };

    public bool spawn = false;
    public bool isSpawning = false;

    public IEnumerator SpawnSword(int swordNums)
    {
        while (swordNums > 0)
        {
            Transform randomSpawnPoint = GetRandomSpawnPoint();

            Transform newSword = BattleManager.Instance.SwordrainSpawner.Spawn(sword, randomSpawnPoint.position, randomSpawnPoint.rotation);

            newSword.gameObject.SetActive(true);

            yield return new WaitForSeconds(waitTime[Random.Range(0, 3)]);

            swordNums--;
        }
    }

    protected virtual Transform GetRandomSpawnPoint()
    {
        return null;
    }
}