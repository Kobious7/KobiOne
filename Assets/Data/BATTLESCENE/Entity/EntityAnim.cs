using UnityEngine;

namespace Battle
{
    public class EntityAnim : EntityAb
    {
        [SerializeField] private bool idle = true;
        [SerializeField] private bool moveTT;
        [SerializeField] private bool moveB;

        public bool MoveB
        {
            get { return moveB; }
            set { moveB = value; }
        }

        [SerializeField] private bool attack;
        [SerializeField] private bool dealSlash;

        public void IdleAnim()
        {
            Entity.Animator.SetInteger("state", 0);
        }

        public void RunAnim()
        {
            Entity.Animator.SetInteger("state", 1);
            if (moveB) Entity.Model.localScale = new Vector3(-1, 1, 1);
        }

        public void DealSlashAnim()
        {
            Entity.Animator.SetInteger("state", 2);
        }

        public float GetAnimDuration(string name)
        {
            foreach (AnimationClip clip in Entity.Animator.runtimeAnimatorController.animationClips)
            {
                if (clip.name == name) return clip.length;
            }

            return 1;
        }
    }
}