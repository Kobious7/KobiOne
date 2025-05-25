using UnityEngine;

public class Opponent : GMono
{
    [SerializeField] private Transform model;

    public Transform Model => model;

    [SerializeField] private Animator animator;

    public Animator Animator => animator;

    [SerializeField] private OpponentStats stats;

    public OpponentStats Stats => stats;

    [SerializeField] private OpponentMoving moving;

    public OpponentMoving Moving => moving;

    [SerializeField] private CapsuleCollider capCollider;

    public CapsuleCollider CapCollider => capCollider;

    [SerializeField] private OpponentAnim anim;

    public OpponentAnim Anim => anim;

    [SerializeField] private OpponentAttack attack;

    public OpponentAttack Atack => attack;

    [SerializeField] private OpponentSwordrain eSwordrain;

    public OpponentSwordrain ESwordrain => eSwordrain;

    [SerializeField] private OpponentPick opponentPick;

    public OpponentPick OpponentPick => opponentPick;

    [SerializeField] private OpponentMoveTile opponentMoveTile;

    public OpponentMoveTile OpponentMoveTile => opponentMoveTile;

    [SerializeField] private OpponentDestroyAndFill opponentDestroyAndFill;

    public OpponentDestroyAndFill OpponentDestroyAndFill => opponentDestroyAndFill;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadOpponentStats();
        LoadOpponentMoving();
        LoadCollider();
        LoadAnimator();
        LoadModel();
        LoadAnimation();
        LoadAttack();
        LoadSwordrain();
        LoadOpponentPick();
        LoadOpponentMoveTile();
        LoadOpponentDestroyAndFill();
    }

    private void LoadOpponentStats()
    {
        if (stats != null) return;

        stats = GetComponentInChildren<OpponentStats>();
    }

    private void LoadOpponentMoving()
    {
        if (moving != null) return;

        moving = GetComponentInChildren<OpponentMoving>();
    }

    private void LoadCollider()
    {
        if (capCollider != null) return;

        capCollider = GetComponent<CapsuleCollider>();
    }

    private void LoadAnimator()
    {
        if (animator != null) return;

        animator = transform.Find("Model").GetChild(0).GetComponent<Animator>();
    }

    protected void LoadModel()
    {
        if (model != null) return;

        model = transform.Find("Model");
    }

    private void LoadAnimation()
    {
        if (anim != null) return;

        anim = transform.GetComponentInChildren<OpponentAnim>();
    }

    private void LoadAttack()
    {
        if (attack != null) return;

        attack = GetComponentInChildren<OpponentAttack>();
    }

    private void LoadSwordrain()
    {
        if (eSwordrain != null) return;

        eSwordrain = GetComponentInChildren<OpponentSwordrain>();
    }

    private void LoadOpponentPick()
    {
        if (opponentPick != null) return;

        opponentPick = GetComponentInChildren<OpponentPick>();
    }

    private void LoadOpponentMoveTile()
    {
        if (opponentMoveTile != null) return;

        opponentMoveTile = GetComponentInChildren<OpponentMoveTile>();
    }

    private void LoadOpponentDestroyAndFill()
    {
        if (opponentDestroyAndFill != null) return;

        opponentDestroyAndFill = GetComponentInChildren<OpponentDestroyAndFill>();
    }
}