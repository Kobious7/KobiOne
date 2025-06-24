using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAnimationEvents : GMono
{
    private static PlayerAnimationEvents instance;

    public static PlayerAnimationEvents Instance => instance;

    private IEntityMeleeAttack meleeAttack;
    private IEntityShooting shooting;
    private BSkillActivator skillActivator;
    private BSkill bSkill;
    [SerializeField] private bool hit;

    public bool Hit
    {
        get => hit;
        set => hit = value;
    }

    protected override void Awake()
    {
        base.Awake();

        if (instance != null) Debug.LogError("Only 1 PlayerAnimationEvents is allowed to exist!");

        instance = this;
    }

    protected override void Start()
    {
        base.Start();

        string activeScene = SceneManager.GetActiveScene().name;

        if (activeScene == "InfiniteMap")
        {
            meleeAttack = InfiniteMapManager.Instance.Player.MeleeAttack;
            shooting = InfiniteMapManager.Instance.Player.Shooting;
        }

        if (activeScene == "Battle")
        {
            meleeAttack = BattleManager.Instance.Player.MeleeAttack;
            shooting = BattleManager.Instance.Player.Shooting;
            bSkill = BSkill.Instance;
            skillActivator = bSkill.SkillActivator;
        }
    }

    public void CheckBasicSlashHit()
    {
        StartCoroutine(meleeAttack.CheckHit());
    }

    public void Shooting()
    {
        shooting.Shoot();
    }

    public void OnSwordA1SHit()
    {
        if (skillActivator.CurrentSkill == SkillButton.Q)
        {
            skillActivator.Activators[bSkill.QSkill].DealOpSkillDamage();
        }

        if (skillActivator.CurrentSkill == SkillButton.E)
        {
            skillActivator.Activators[bSkill.ESkill].DealOpSkillDamage();
        }

        if (skillActivator.CurrentSkill == SkillButton.Space)
        {
            skillActivator.Activators[bSkill.SpaceSkill].DealOpSkillDamage();
        }
    }
}