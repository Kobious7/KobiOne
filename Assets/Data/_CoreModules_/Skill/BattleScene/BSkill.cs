using System;
using System.Collections.Generic;
using UnityEngine;

public class BSkill : GMono
{
    private static BSkill instance;

    public static BSkill Instance => instance;

    [SerializeField] private SkillNode qSkill;

    public SkillNode QSkill => qSkill;

    [SerializeField] private SkillNode eSkill;

    public SkillNode ESkill => eSkill;

    [SerializeField] private SkillNode spaceSkill;

    public SkillNode SpaceSkill => spaceSkill;

    [SerializeField] private bool qUnlocking;

    public bool QUnlocking
    {
        get => qUnlocking;
        set => qUnlocking = value;
    }

    [SerializeField] private bool eUnlocking;

    public bool EUnlocking
    {
        get => eUnlocking;
        set => eUnlocking = value;
    }

    [SerializeField] private bool spaceUnlocking;

    public bool SpaceUnlocking
    {
        get => spaceUnlocking;
        set => spaceUnlocking = value;
    }

    [SerializeField] private QSkill q;

    public QSkill Q => q;

    [SerializeField] private ESkill e;

    public ESkill E => e;

    [SerializeField] private SpaceSkill space;

    public SpaceSkill Space => space;

    [SerializeField] private BSkillActivator skillActivator;

    public BSkillActivator SkillActivator => skillActivator;

    [SerializeField] private BSkillDamageCalculator calculator;

    public BSkillDamageCalculator Calculator => calculator;

    private PlayerInfo playerInfo;

    protected override void Awake()
    {
        base.Awake();
        if(instance != null) Debug.LogError("Only 1 BSkill is allowed to exist");

        instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadQ();
        LoadE();
        LoadSpace();
        LoadActivator();
        LoadCalculator();
    }

    protected override void Start()
    {
        base.Start();

        playerInfo = BattleManager.Instance.MapData.PlayerInfo;

        LoadSkillFromSO();
    }

    private void LoadQ()
    {
        if(q != null) return;

        q = GetComponentInChildren<QSkill>();
    }

    private void LoadE()
    {
        if(e != null) return;

        e = GetComponentInChildren<ESkill>();
    }

    private void LoadSpace()
    {
        if(space != null) return;

        space = GetComponentInChildren<SpaceSkill>();
    }

    private void LoadActivator()
    {
        if(skillActivator != null) return;

        skillActivator = GetComponentInChildren<BSkillActivator>();
    }

    private void LoadCalculator()
    {
        if(calculator != null) return;

        calculator = GetComponentInChildren<BSkillDamageCalculator>();
    }

    private void LoadSkillFromSO()
    {
        if(!BattleManager.Instance.MapData.MapCanLoad) return;

        if(playerInfo.QSkill != null)
        {
            qSkill.Level = playerInfo.QSkill.Level;
            qSkill.skillSO = playerInfo.QSkill.skillSO;
        }
        if(playerInfo.ESkill != null)
        {
            eSkill.Level = playerInfo.ESkill.Level;
            eSkill.skillSO = playerInfo.ESkill.skillSO;
        }
        if(playerInfo.SpaceSkill != null)
        {
            spaceSkill.Level = playerInfo.SpaceSkill.Level;
            spaceSkill.skillSO = playerInfo.SpaceSkill.skillSO;
        }
    }
}