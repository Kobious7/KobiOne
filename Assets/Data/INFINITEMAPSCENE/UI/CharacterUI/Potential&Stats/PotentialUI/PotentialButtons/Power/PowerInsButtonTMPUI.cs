using UnityEngine;

namespace InfiniteMap
{
    public class PowerInsButtonTMPUI : InsButtonTMPUI
    {
        protected override void IncreasePotential()
        {
            base.IncreasePotential();

            playerStats.Power++;
            points.text = "" + playerStats.Power;
            playerStats.SlashDamage = playerStats.Power;
            LineStatUI line = StatsUISpawner.Instance.GetLineStatUI(3);
            line.Data.text = "" + playerStats.SlashDamage;
        }
    }
}