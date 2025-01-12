using UnityEngine;

namespace InfiniteMap
{
    public class InsButtonTMPUI : ButtonTMPUI
    {
        protected override void Click()
        {
            base.Click();
            Increase();
        }

        private void Increase()
        {
            playerStats.RemainPoints--;
            PotentialUI.Instance.RemainPoints.text = playerStats.RemainPoints + "";

            IncreasePotential();       
        }

        protected virtual void IncreasePotential()
        {
            //Override
        }
    }
}