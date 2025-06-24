using System.Collections.Generic;
using UnityEngine;

public class TileSkill : GMono
{
    [SerializeField] private TileFindTargets targetsFinder;

    public TileFindTargets TargetsFinder => targetsFinder;

    [SerializeField] private BMonster monster;

    public BMonster Monster => monster;

    [SerializeField] private BPlayer player;

    public BPlayer Player => player;

    protected override void Start()
    {
        base.Start();
        monster = BattleManager.Instance.Monster;
        player = BattleManager.Instance.Player;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadTargetsFinder();
    }

    private void LoadTargetsFinder()
    {
        if(targetsFinder != null) return;

        targetsFinder = GetComponentInChildren<TileFindTargets>();
    }
}