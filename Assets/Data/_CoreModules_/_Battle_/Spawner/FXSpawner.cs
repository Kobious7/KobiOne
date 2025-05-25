using UnityEngine;

public class FXSpawner : Spawner
{
    [SerializeField] private BattleFX fx;

    public BattleFX FX => fx;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadFX();
    }

    private void LoadFX()
    {
        if (fx != null) return;

        fx = transform.Find("Prefabs").Find("TileExplode").GetComponent<BattleFX>();
    }
}