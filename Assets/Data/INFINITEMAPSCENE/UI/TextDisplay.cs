using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InfiniteMap
{
    public class TextDisplay : GMono
    {
        [SerializeField] private TextMeshProUGUI text;

        public TextMeshProUGUI Text => text;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadText();
        }

        private void LoadText()
        {
            if (text != null) return;

            text = GetComponentInChildren<TextMeshProUGUI>();
        }
    }
}