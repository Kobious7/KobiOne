using UnityEngine;

namespace Battle
{
    public class ELock : SkillUILock
    {
        protected override void GetConsume()
        {
            base.GetConsume();
            consume = Game.Instance.PlayerSkill[1].Properties.Consume;
        }
    }
}