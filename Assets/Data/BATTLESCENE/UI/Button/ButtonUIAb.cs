using UnityEngine;

namespace Battle
{
    public abstract class ButtonUIAb : GMono
    {
        [SerializeField] private ButtonUI buttonUI;

        public ButtonUI ButtonUI => buttonUI;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadButtonUI();
        }

        private void LoadButtonUI()
        {
            if (buttonUI != null) return;

            buttonUI = transform.parent.GetComponent<ButtonUI>();
        }
    }
}