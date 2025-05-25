using UnityEngine;

public class QSelf : GMono
{
    [SerializeField] private Player player;

    public Player Player => player;

    [SerializeField] private Opponent opponent;

    public Opponent Opponent => opponent;

    protected override void Start()
    {
        base.Start();

        player = Game.Instance.Player;
        opponent = Game.Instance.Opponent;
    }
}
