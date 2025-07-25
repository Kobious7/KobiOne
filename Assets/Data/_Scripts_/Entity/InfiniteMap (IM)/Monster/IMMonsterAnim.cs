using UnityEngine;

public class IMMonsterAnim : EntityAnim
{
    public void BeingHit()
    {
        Entity.Animator.Play("BeingHit");
    }
}