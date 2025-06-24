using System.Collections.Generic;
using UnityEngine;

public class QTile : GMono
{
    [SerializeField] private QTileFindTargets targetsFinder;

    public QTileFindTargets TargetsFinder => targetsFinder;

    [SerializeField] private Transform monster;

    public Transform Monster => monster;

    [SerializeField] private BPlayer player;

    public BPlayer Player => player;

    protected override void Start()
    {
        base.Start();
        monster = BattleManager.Instance.Monster.transform;
        player = BattleManager.Instance.Player;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadTargetsFinder();
    }

    private void LoadTargetsFinder()
    {
        if (targetsFinder != null) return;

        targetsFinder = GetComponentInChildren<QTileFindTargets>();
    }
}