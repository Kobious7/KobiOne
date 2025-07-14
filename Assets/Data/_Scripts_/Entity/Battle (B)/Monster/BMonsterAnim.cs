using UnityEngine;

public class BMonsterAnim : BEntityAnim
{
    protected override void Start()
    {
        base.Start();

        Battle.Instance.OnMonsterLost += Die;
    }

    public void MeleeAttack()
    {
        Entity.Animator.SetTrigger("melee_attack");
    }

    public void RangedAttack()
    {
        Entity.Animator.SetTrigger("ranged_attack");
    }

    public void BeingHit()
    {
        Entity.Animator.Play("BeingHit");
    }

    public void Die()
    {
        Entity.Animator.SetInteger("state", (-1));
    }
}