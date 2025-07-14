using UnityEngine;

public class IMPlayerRangedAttack : EntityComponent
{
    [SerializeField] private float range = 1;
    [SerializeField] private float timer = 1;
    [SerializeField] private float counter = 0;
    [SerializeField] private bool canAttack;
    private IMPlayer player;
    private IMPlayerAnim anim;

    protected override void Start()
    {
        base.Start();
        player = Entity as IMPlayer;
        anim = Entity.Anim as IMPlayerAnim;
    }

    private void Update()
    {
        if (player.IsUIOpening) return;

        CountTime();

        if (InputManager.Instance.Fire1 > 0 && canAttack)
        {
            RangedAttack();
        }
    }

    private void CountTime()
    {
        if (counter >= timer)
        {
            canAttack = true;
            return;
        }

        counter += Time.deltaTime;
    }

    private void RangedAttack()
    {
        canAttack = false;
        counter = 0;

        anim.RangedAttack();
    }
}