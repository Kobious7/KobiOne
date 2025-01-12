using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace InfiniteMap
{
    public class LineStatUI : GMono
    {
        [SerializeField] private TextMeshProUGUI text;

        public TextMeshProUGUI Text => text;

        [SerializeField] private TextMeshProUGUI data;

        public TextMeshProUGUI Data => data;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadTextAndData();
        }

        private void LoadTextAndData()
        {
            if(text != null && data != null) return;

            text = transform.Find("Text").GetComponent<TextMeshProUGUI>();
            data = transform.Find("DataText").GetComponent<TextMeshProUGUI>();
        }

        public void ShowData(StatUI stat)
        {
            text.text = stat.Text + ": ";
            data.text = stat.Data + "";
            //text.transform.AddComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            //data.transform.AddComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        }
    }
}