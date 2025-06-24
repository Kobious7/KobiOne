using UnityEngine;

public class Entity : GMono
{
    [SerializeField] private Transform model;

    public Transform Model => model;

    [SerializeField] private Transform rigModel;

    public Transform RigModel => rigModel;

    [SerializeField] private Animator animator;

    public Animator Animator => animator;

    [SerializeField] private EntityAnim anim;

    public EntityAnim Anim => anim;

    [SerializeField] private Transform centerPoint;

    public Transform CenterPoint => centerPoint;

    [SerializeField] private EntityAttackPoint attackPoint;

    public EntityAttackPoint AttackPoint => attackPoint;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadModel();
        LoadRigModel();
        LoadAnimator();
        LoadAnimation();
        LoadCenterPoint();
        LoadAttackPoint();
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

        anim = GetComponentInChildren<EntityAnim>();
    }

    private void LoadCenterPoint()
    {
        if (centerPoint != null) return;

        centerPoint = transform.Find("CenterPoint");
    }

    private void LoadAttackPoint()
    {
        if (attackPoint != null) return;

        attackPoint = GetComponentInChildren<EntityAttackPoint>();
    }
}