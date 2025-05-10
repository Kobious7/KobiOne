using UnityEngine;

namespace InfiniteMap
{
    public class MonsterStats : MonsterAb
    {
        [SerializeField] private int level;

        public int Level
        {
            get { return level; }
            set { level = value; }
        }

        [SerializeField] private int maxHP;

        public int MaxHP
        {
            get { return maxHP; }
            set { maxHP = value; }
        }

        [SerializeField] private int swordrainDamage;

        public int SwordrainDamage
        {
            get { return swordrainDamage; }
            set { swordrainDamage = value; }
        }

        [SerializeField] private int slashDamage;

        public int SlashDamage
        {
            get { return slashDamage; }
            set { slashDamage = value; }
        }

        [SerializeField] private bool loadFromData;

        protected override void OnEnable()
        {
            base.OnEnable();
            level = Random.Range(1, 10);
            maxHP = 30 + level * 10;
            slashDamage = level;
            swordrainDamage = level;
            if(Game.Instance.MapData.InfiniteMapInfo.ReloadMonster)
            {
                loadFromData = true;
            }

            if(loadFromData)
            {
                UpdateStats(Game.Instance.MapData.InfiniteMapInfo.MapLevel);
                loadFromData = false;
            }
            else
            {
                UpdateStats(Game.Instance.Map.MapLevel.CurrentLevel);
            }
        }

        private void UpdateStats(int mapLevel)
        {
            level += 10 * mapLevel;
            maxHP = 30 + level * 10;
            slashDamage = level;
            swordrainDamage = level;
        }
    }
}