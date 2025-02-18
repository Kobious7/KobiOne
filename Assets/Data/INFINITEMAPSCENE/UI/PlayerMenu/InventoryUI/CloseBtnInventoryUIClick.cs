using UnityEngine;

namespace InfiniteMap
{
    public class CloseBtnInventoryUIClick : ButtonUIClick
    {
        protected override void Click()
        {
            base.Click();
            GameUI.Instance.InventoryUI.Close();
        }
    }
}