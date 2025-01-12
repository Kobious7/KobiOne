using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace InfiniteMap
{
    public class StatsUISpawner : Spawner
    {

        private static StatsUISpawner instance;

        public static StatsUISpawner Instance => instance;

        [SerializeField] private List<StatUI> stats;

        [SerializeField] private List<LineStatUI> lineStatUIs;

        private PlayerStats playerStats;

        protected override void Awake()
        {
            base.Awake();
            if(instance != null) Debug.LogError("Only 1 StatUISpawner is allowed to exist!");

            instance = this;
        }

        protected override void LoadHolder()
        {
            if(holder != null) return;

            holder = GetComponent<ScrollRect>().content;
        }

        protected override void Start()
        {
            base.Start();
            playerStats = Game.Instance.Player.Stats;
            playerStats.MaxHP = playerStats.Strength * 10;
            playerStats.SlashDamage = playerStats.Power;
            playerStats.SwordrainDamage = playerStats.Magic / 3;
            CreateStatList();
            SpawnStatLine();
            GetStatUIList();
        }

        private void CreateStatList()
        {
            stats = new List<StatUI>
            {
                new StatUI(0, "Level", playerStats.Level),
                new StatUI(1, "Exp", playerStats.RequiredExp),
                new StatUI(2, "Max HP", playerStats.MaxHP),
                new StatUI(3, "Slash Damage", playerStats.SlashDamage),
                new StatUI(4, "Swordrain Damage", playerStats.SwordrainDamage)
            };
        }

        private void SpawnStatLine()
        {
            foreach(var stat in stats)
            {
                Transform newStat = Spawn(prefabs[0], Vector3.zero, Quaternion.identity);
                newStat.transform.localScale = Vector3.one;
                LineStatUI line = newStat.GetComponent<LineStatUI>();

                line.ShowData(stat);
                newStat.gameObject.SetActive(true);
            }
        }

        public void GetStatUIList()
        {
            lineStatUIs = holder.GetComponentsInChildren<LineStatUI>().ToList();
        }

        public LineStatUI GetLineStatUI(int index)
        {
            return lineStatUIs[index];
        }
    }
}