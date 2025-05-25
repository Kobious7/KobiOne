using System.Collections.Generic;
using UnityEngine;

public class TileSkill : GMono
{
    [SerializeField] private TileFindTargets targetsFinder;

    public TileFindTargets TargetsFinder => targetsFinder;

    [SerializeField] private Transform opponent;

    public Transform Opponent => opponent;

    [SerializeField] private Player player;

    public Player Player => player;

    protected override void Start()
    {
        base.Start();
        opponent = Game.Instance.Opponent.transform;
        player = Game.Instance.Player;
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