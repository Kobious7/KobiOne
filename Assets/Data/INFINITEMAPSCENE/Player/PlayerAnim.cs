using UnityEngine;

namespace InfiniteMap
{
    public class PlayerAnim : PlayerAb
    {
        public void IdleAnim()
        {
            Player.Animator.SetInteger("state", 0);
        }

        public void RunAnim()
        {
            Player.Animator.SetInteger("state", 1);
        }

        public void MeleeAttack()
        {
            Player.Animator.SetTrigger("attack");
        }
    }
}