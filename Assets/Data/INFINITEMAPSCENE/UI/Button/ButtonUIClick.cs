using UnityEngine;

namespace InfiniteMap
{
    public class ButtonUIClick : ButtonUIAb
    {
        protected override void Start()
        {
            base.Start();
            ButtonUI.Btn.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            Click();
        }

        protected virtual void Click()
        {
            //Override
        }
    }
}