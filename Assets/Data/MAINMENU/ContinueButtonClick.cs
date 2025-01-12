using UnityEngine;

namespace MainMenu
{
    public class ContinueButtonClick : ButtonClick
    {
        protected override void Click()
        {
            base.Click();
            LoadScene("InfiniteMap");
        }
    }
}