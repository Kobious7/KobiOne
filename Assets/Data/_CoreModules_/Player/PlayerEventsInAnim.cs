using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEventsInAnim : GMono
{
    private static PlayerEventsInAnim instance;

    public static PlayerEventsInAnim Instance => instance;

    private PlayerMeleeAttack meleeAttack;
    private PlayerShooting playerShooting;
    [SerializeField] private bool hit;
    private PlayerBattleMovement battleMovement;

    public bool Hit
    {
        get => hit;
        set => hit = value;
    }

    protected override void Awake()
    {
        base.Awake();

        if (instance != null) Debug.LogError("Only 1 PlayerEventsInAnim is allowed to exist!");

        instance = this;
    }

    protected override void Start()
    {
        base.Start();
        meleeAttack = Game.Instance.Player.MeleeAttack;
        playerShooting = Game.Instance.Player.Shooting;
    }

    public void CheckBasicSlashHit()
    {
        StartCoroutine(meleeAttack.CheckHit());
    }

    public void Shooting()
    {
        playerShooting.Shoot();
    }
}