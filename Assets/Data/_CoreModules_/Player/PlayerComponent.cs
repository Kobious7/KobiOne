using UnityEngine;

public abstract class PlayerComponent : GMono
{
    [SerializeField] private Player player;

    public Player Player => player;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPlayer();
    }

    private void LoadPlayer()
    {
        if (player != null) return;

        player = transform.parent.GetComponent<Player>();
    }
}