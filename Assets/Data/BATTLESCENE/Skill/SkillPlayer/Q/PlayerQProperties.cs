using UnityEngine;

namespace Battle
{
    public class PlayerQProppeties : SkillProperties
    {
        protected override void GetProperties()
        {
            base.GetProperties();
            int[,] newRange = { { 0, 0 } };
            range = newRange;
            consume = 25;
            spawnCount = 4;
        }
    }
}