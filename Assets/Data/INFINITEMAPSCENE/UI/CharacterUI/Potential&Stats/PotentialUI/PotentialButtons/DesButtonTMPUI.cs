using UnityEngine;

namespace InfiniteMap
{
    public class DesButtonTMPUI : ButtonTMPUI
    {
        protected override void Click()
        {
            base.Click();
            Descrease();
        }

        private void Descrease()
        {
            playerStats.RemainPoints++;
            PotentialUI.Instance.RemainPoints.text = playerStats.RemainPoints + "";

            DescreasePotential();       
        }

        protected virtual void DescreasePotential()
        {
            //Override
        }
    }
}