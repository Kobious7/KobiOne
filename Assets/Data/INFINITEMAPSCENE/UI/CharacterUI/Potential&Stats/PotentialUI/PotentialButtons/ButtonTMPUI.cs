using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InfiniteMap
{
    public class ButtonTMPUI : GMono
    {
        [SerializeField] protected TextMeshProUGUI points;

        public TextMeshProUGUI Points => points;

        [SerializeField] protected Button btn;

        public Button Btn => btn;

        protected PlayerStats playerStats;

        protected override void Start()
        {
            base.Start();
            playerStats = Game.Instance.Player.Stats;
            btn.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            Click();
        }

        protected virtual void Click()
        {
            //Override 
        }

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadPoints();
            LoadButton();
        }

        private void LoadPoints()
        {
            if(points != null) return;

            points = transform.parent.Find("Points").GetComponent<TextMeshProUGUI>();
        }

        private void LoadButton()
        {
            if(btn != null) return;

            btn = GetComponent<Button>();
        }
    }
}