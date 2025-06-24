using UnityEngine;

public class BMonsterDestroyAndFill : EntityComponent
{
    private BoardDestroyedMatches boardDestroyedMatches;
    private BMonster monster;

    protected override void Start()
    {
        base.Start();

        monster = Entity as BMonster;
    }

    public void DestroyAndFill()
    {
        monster.MoveTile.MoveTile();

        boardDestroyedMatches = BattleManager.Instance.Board.BoardDestroyedMatches;

        StartCoroutine(boardDestroyedMatches.DestroyAndFill());
    }
}