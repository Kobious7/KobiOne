using UnityEngine;

namespace InfiniteMap
{
    public abstract class MonsterAb : GMono
    {
        [SerializeField] private Monster monster;

        public Monster Monster => monster;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadMonster();
        }

        private void LoadMonster()
        {
            if (monster != null) return;

            monster = transform.parent.GetComponent<Monster>();
        }
    }
}