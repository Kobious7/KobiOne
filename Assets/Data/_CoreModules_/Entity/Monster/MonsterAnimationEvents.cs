using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterAnimationEvents : GMono
{
    private static MonsterAnimationEvents instance;

    public static MonsterAnimationEvents Instance => instance;

    private IEntityMeleeAttack meleeAttack;
    private IEntityShooting shooting;

    [SerializeField] private bool hit;

    public bool Hit
    {
        get => hit;
        set => hit = value;
    }

    protected override void Awake()
    {
        base.Awake();

        if (instance != null) Debug.LogError("Only 1 MonsterAnimationEvents is allowed to exist!");

        instance = this;
    }

    protected override void Start()
    {
        base.Start();

        meleeAttack = BattleManager.Instance.Monster.MeleeAttack;
        shooting = BattleManager.Instance.Monster.Shooting;
    }

    public void CheckBasicSlashHit()
    {
        StartCoroutine(meleeAttack.CheckHit());
    }

    public void Shooting()
    {
        shooting.Shoot();
    }
}