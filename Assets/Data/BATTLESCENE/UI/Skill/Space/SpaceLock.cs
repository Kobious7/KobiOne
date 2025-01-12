using UnityEngine;

namespace Battle
{
    public class SpaceLock : SkillUILock
    {
        protected override void GetConsume()
        {
            consume = Game.Instance.PlayerSkill[2].Properties.Consume;
        }
    }
}