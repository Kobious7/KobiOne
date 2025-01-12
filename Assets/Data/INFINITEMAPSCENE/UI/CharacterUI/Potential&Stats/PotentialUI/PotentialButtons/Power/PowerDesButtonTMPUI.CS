using UnityEngine;

namespace InfiniteMap
{
    public class PowerDesButtonTMPUI : DesButtonTMPUI
    {
        protected override void DescreasePotential()
        {
            base.DescreasePotential();

            playerStats.Power--;
            points.text = "" + playerStats.Power;
            playerStats.SlashDamage = playerStats.Power;
            LineStatUI line = StatsUISpawner.Instance.GetLineStatUI(3);
            line.Data.text = "" + playerStats.SlashDamage;
        }
    }
}