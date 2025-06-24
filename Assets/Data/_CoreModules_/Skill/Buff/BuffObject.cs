using UnityEngine;

public class BuffObject : GMono
{
    [SerializeField] private BEntityStats stats;

    public BEntityStats Stats
    {
        get => stats;
        set => stats = value;
    }

    [SerializeField] private EquipStatType sourceStat;

    public EquipStatType SourceStat
    {
        get => sourceStat;
        set => sourceStat = value;
    }

    [SerializeField] private EquipStatType trueStatBuff;

    public EquipStatType TrueStatBuff
    {
        get => trueStatBuff;
        set => trueStatBuff = value;
    }

    [SerializeField] private DamageType damageType;

    public DamageType DamageType
    {
        get => damageType;
        set => damageType = value;
    }

    [SerializeField] private int percentBuff;

    public int PercentBuff
    {
        get => percentBuff;
        set => percentBuff = value;
    }

    [SerializeField] private DurationType durationType;

    public DurationType DurationType
    {
        get => durationType;
        set => durationType = value;
    }

    [SerializeField] private int duration;

    public int Duration
    {
        get => duration;
        set => duration = value;
    }

    [SerializeField] private bool durationStack;

    public bool DurationStack
    {
        get => durationStack;
        set => durationStack = value;
    }

    [SerializeField] private bool percentStack;

    public bool PercentStack
    {
        get => percentStack;
        set => percentStack = value;
    }

    [SerializeField] private BuffObjectHandling buffHandler;

    public BuffObjectHandling BuffHandler => buffHandler;

    [SerializeField] private SkillActivator activator;

    public SkillActivator Activator
    {
        get => activator;
        set => activator = value;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBuffHanlder();
    }

    private void LoadBuffHanlder()
    {
        if(buffHandler != null) return;

        buffHandler = GetComponentInChildren<BuffObjectHandling>();
    }
}