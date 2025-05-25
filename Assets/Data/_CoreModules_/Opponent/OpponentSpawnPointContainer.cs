using UnityEngine;

public class OpponentSpawnPointContainer : GMono
{
    [SerializeField] private SwordrainSpawnPoint[] swordrainSpawnPoint;

    public SwordrainSpawnPoint[] SwordrainSpawnPoints => swordrainSpawnPoint;

    protected override void LoadComponents()
    {
        LoadSpawnPoints();
    }

    private void LoadSpawnPoints()
    {
        if (swordrainSpawnPoint.Length > 0) return;

        swordrainSpawnPoint = GetComponentsInChildren<SwordrainSpawnPoint>();
    }

    public Transform GetRandomSpawnPoint()
    {
        return swordrainSpawnPoint[Random.Range(0, swordrainSpawnPoint.Length)].transform;
    }
}