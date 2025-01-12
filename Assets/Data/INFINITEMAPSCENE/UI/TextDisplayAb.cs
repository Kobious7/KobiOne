using UnityEngine;

namespace InfiniteMap
{
    public abstract class TextDisplayAb : GMono
    {
        [SerializeField] protected TextDisplay text;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadTextDisplay();
        }

        private void LoadTextDisplay()
        {
            if (text != null) return;

            text = transform.parent.GetComponent<TextDisplay>();
        }
    }
}