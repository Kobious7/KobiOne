using UnityEngine;

namespace Battle
{
    public class QLock : SkillUILock
    {
        protected override void GetConsume()
        {
            consume = Game.Instance.PlayerSkill[0].Properties.Consume;
        }
    }
}