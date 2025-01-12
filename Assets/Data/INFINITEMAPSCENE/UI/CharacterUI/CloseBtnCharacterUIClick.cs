using UnityEngine;

namespace InfiniteMap
{
    public class CloseBtnCharacterUIClick : ButtonUIClick
    {
        protected override void Click()
        {
            base.Click();
            GameUI.Instance.CharacterUI.Close();
        }
    }
}