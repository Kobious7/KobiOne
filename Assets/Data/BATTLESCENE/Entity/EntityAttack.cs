using System.Collections;
using UnityEngine;

namespace Battle
{
    public class EntityAttack : EntityAb
    {
        [SerializeField] protected bool doMelee;
        [SerializeField] protected bool doRange;
        [SerializeField] private LayerMask mask;

        public IEnumerator MeleeAttack()
        {
            Entity.Anim.DealSlashAnim();
            yield return new WaitForSeconds(Entity.Anim.GetAnimDuration("MeleeAttackAnim"));
        }

        public virtual void RangeAttack()
        {

        }
    }
}