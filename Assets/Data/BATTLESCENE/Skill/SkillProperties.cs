using UnityEngine;

namespace Battle
{
    public class SkillProperties : SkillAb
    {
        [SerializeField] protected int[,] range;

        public int[,] Range => range;

        [SerializeField] protected int consume;

        public int Consume => consume;

        [SerializeField] protected int spawnCount;

        public int SpawnCount => spawnCount;

        protected override void ResetValues()
        {
            base.ResetValues();
            GetProperties();
        }

        protected virtual void GetProperties()
        {
            //Override
        }
    }
}