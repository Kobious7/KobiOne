using UnityEngine;

public abstract class BEntity : GMono
{
    [SerializeField] private Transform model;

    public Transform Model => model;

    [SerializeField] private Transform rigModel;

    public Transform RigModel => rigModel;

    [SerializeField] private Animator animator;

    public Animator Animator => animator;

    [SerializeField] private BEntityAnim anim;

    public BEntityAnim Anim => anim;

    [SerializeField] private Transform centerPoint;

    public Transform CenterPoint => centerPoint;

    [SerializeField] private BEntityAttackPoint attackPoint;

    public BEntityAttackPoint AttackPoint => attackPoint;

    [SerializeField] private Transform textDamagePoint;

    public Transform TextDamagePoint => textDamagePoint;
    [SerializeField] private CapsuleCollider capsuleCollider;

    public CapsuleCollider CapsuleCollider => capsuleCollider;

    [SerializeField] private BEntityMovement movement;

    public BEntityMovement Movement => movement;

    [SerializeField] private BEntityAttackRange attackRange;

    public BEntityAttackRange AttackRange => attackRange;

    [SerializeField] private BEntityShooting shooting;

    public BEntityShooting Shooting => shooting;

    [SerializeField] private BEntityStats stats;

    public BEntityStats Stats => stats;

    [SerializeField] private BEntitySwordrain swordrain;

    public BEntitySwordrain Swordrain => swordrain;

    [SerializeField] private BEntityShield shield;

    public BEntityShield Shield => shield;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadModel();
        LoadRigModel();
        LoadAnimator();
        LoadAnimation();
        LoadCenterPoint();
        LoadAttackPoint();
        LoadTextDamagePoint();
        LoadCapsuleCollider();
        LoadMovement();
        LoadAttackRange();
        LoadShooting();
        LoadStats();
        LoadSwordrain();
        LoadShield();
    }

    private void LoadModel()
    {
        if (model != null) return;

        model = transform.Find("Model");
    }

    private void LoadRigModel()
    {
        if (rigModel != null) return;

        rigModel = model.GetChild(0);
    }

    private void LoadAnimator()
    {
        if (animator != null) return;

        animator = rigModel.GetComponent<Animator>();
    }

    private void LoadAnimation()
    {
        if (anim != null) return;

        anim = GetComponentInChildren<BEntityAnim>();
    }

    private void LoadCenterPoint()
    {
        if (centerPoint != null) return;

        centerPoint = transform.Find("CenterPoint");
    }

    private void LoadAttackPoint()
    {
        if (attackPoint != null) return;

        attackPoint = GetComponentInChildren<BEntityAttackPoint>();
    }

    private void LoadTextDamagePoint()
    {
        if (textDamagePoint != null) return;

        textDamagePoint = transform.Find("TextDamagePoint");
    }

    private void LoadCapsuleCollider()
    {
        if (capsuleCollider != null) return;

        capsuleCollider = GetComponentInChildren<CapsuleCollider>();
    }

    private void LoadMovement()
    {
        if (movement != null) return;

        movement = GetComponentInChildren<BEntityMovement>();
    }

    private void LoadAttackRange()
    {
        if (attackRange != null) return;

        attackRange = GetComponentInChildren<BEntityAttackRange>();
    }

    private void LoadShooting()
    {
        if (shooting != null) return;

        shooting = GetComponentInChildren<BEntityShooting>();
    }

    private void LoadStats()
    {
        if (stats != null) return;

        stats = GetComponentInChildren<BEntityStats>();
    }

    private void LoadSwordrain()
    {
        if (swordrain != null) return;

        swordrain = GetComponentInChildren<BEntitySwordrain>();
    }

    private void LoadShield()
    {
        shield = GetComponentInChildren<BEntityShield>();

        shield.gameObject.SetActive(false);
    }
}