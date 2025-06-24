using UnityEngine;

public class BattleFXDespawn : BattleFXAb
{
    [SerializeField] private float time = 0;
    [SerializeField] private bool despawn;

    public bool Despawn
    {
        get => despawn;
        set => despawn = value;
    }

    private void Update()
    {
        DespawnFX();
    }

    private void DespawnFX()
    {
        if (!despawn) return;

        time += Time.deltaTime;

        if (time >= FX.Anim.GetAnimClipTime())
        {
            time = 0;
            despawn = false;
            BattleManager.Instance.FXSpawner.Despawn(transform.parent);
        }
    }
}