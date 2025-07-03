using UnityEngine;

public class BuffInfo : GMono
{
    [SerializeField] private int index;

    public int Index
    {
        get => index;
        set => index = value;
    }

    [SerializeField] private Sprite icon;

    public Sprite Icon
    {
        get => icon;
        set => icon = value;
    }

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

    [SerializeField] private float percentBuff;

    public float PercentBuff
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

    [SerializeField] private string description;

    public string Description
    {
        get => description;
        set => description = value;
    }

    [SerializeField] private SkillActivator activator;

    public SkillActivator Activator
    {
        get => activator;
        set => activator = value;
    }
}