using UnityEngine;

public class IMMonster : Entity
{
    [SerializeField] private bool isBeingHit = false;

    public bool IsBeingHit { get => isBeingHit; set => isBeingHit = value; }
    
    [SerializeField] private IMMonsterStats stats;

    public IMMonsterStats Stats => stats;

    [SerializeField] private IMMonsterDropList dropList;

    public IMMonsterDropList DropList => dropList;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadStats();
        LoadDropList();
    }

    private void LoadStats()
    {
        if (stats != null) return;

        stats = GetComponentInChildren<IMMonsterStats>();
    }

    private void LoadDropList()
    {
        if (dropList != null) return;

        dropList = GetComponentInChildren<IMMonsterDropList>();
    }
}