using UnityEngine;

namespace Battle
{
    public class PlayerEProperties : SkillProperties
    {
        protected override void GetProperties()
        {
            base.GetProperties();
            int[,] newRange = { { 0, 0 }, { 1, 0 }, { 0, -1 }, { 1, -1 } };
            range = newRange;
            consume = 50;
            spawnCount = 3;
        }
    }
}