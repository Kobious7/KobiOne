using UnityEngine;

public class OpponentComponent : GMono
{
    [SerializeField] private Opponent opponent;

    public Opponent Opponent => opponent;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadOpponent();
    }

    private void LoadOpponent()
    {
        if (opponent != null) return;

        opponent = transform.parent.GetComponent<Opponent>();
    }
}