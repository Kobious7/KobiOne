using UnityEngine;

namespace Battle
{
    public class PlayerSpaceProperties : SkillProperties
    {
        protected override void GetProperties()
        {
            base.GetProperties();
            int[,] newRange = { { -1, 1 }, { 0, 1 }, { 1, 1 }, { -1, 0 }, { 0, 0 }, { 1, 0 }, { -1, -1 }, { 0, -1 }, { 1, -1 } };
            range = newRange;
            consume = 75;
            spawnCount = 2;
        }
    }
}