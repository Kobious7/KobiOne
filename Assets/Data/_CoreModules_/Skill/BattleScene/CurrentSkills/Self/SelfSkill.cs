using UnityEngine;

public class SelfSkill : GMono
{
    [SerializeField] private BPlayer player;

    public BPlayer Player => player;

    [SerializeField] private BMonster monster;

    public BMonster Monster => monster;

    protected override void Start()
    {
        base.Start();

        player = BattleManager.Instance.Player;
        monster = BattleManager.Instance.Monster;
    }
}