using System.Collections;
using UnityEngine;

public class IMPlayerMeleeAttack : EntityComponent, IEntityMeleeAttack
{
    [SerializeField] protected float range = 1;
    [SerializeField] protected LayerMask layerMask;
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
            MeleeAttack();
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

    protected void MeleeAttack()
    {
        canAttack = false;
        counter = 0;

        anim.MeleeAttack();
    }

    public IEnumerator CheckHit()
    {
        Collider[] monsters = Physics.OverlapSphere(player.CenterPoint.position, range, layerMask);

        if (monsters.Length > 0)
        {
            PlayerAnimationEvents.Instance.Hit = true;
        }

        foreach (Collider monster in monsters)
        {
            yield return new WaitForSeconds(1.5f);

            //LoadScene("Battle");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(Entity.CenterPoint.position, range);
    }
}