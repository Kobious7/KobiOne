using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InfiniteMap
{
    public class LineStatUI : GMono
    {
        [SerializeField] private TextMeshProUGUI text;

        public TextMeshProUGUI Text => text;

        [SerializeField] private TextMeshProUGUI value;

        public TextMeshProUGUI Value => value;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadTextAndValue();
        }

        private void LoadTextAndValue()
        {
            if(text != null && value != null) return;

            text = transform.Find("Text").GetComponent<TextMeshProUGUI>();
            value = transform.Find("Value").GetComponent<TextMeshProUGUI>();
        }

        public void ShowData(Stat stat)
        {
            if(stat.IsPercentValue)
            {
                text.text = stat.Name + ": ";
                value.text = $"{stat.PercentBonus:F1}%";
            }
            else
            {
                text.text = stat.Name + ": ";
                value.text = stat.Value + "";
            }
        }
    }
}