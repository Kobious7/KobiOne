using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Battle
{
    public class MP : GMono
    {
        [SerializeField] private Image model;

        public Image Model => model;

        [SerializeField] private TextMeshProUGUI textMP;

        public TextMeshProUGUI TextMP => textMP;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadModel();
            LoadText();
        }

        private void LoadModel()
        {
            if (model != null) return;

            model = transform.Find("Model").GetComponent<Image>();
        }

        private void LoadText()
        {
            if (textMP != null) return;

            textMP = GetComponentInChildren<TextMeshProUGUI>();
        }
    }
}