using TMPro;
using UnityEngine;

namespace InfiniteMap
{
    public class PotentialUI : GMono
    {
        private static PotentialUI instance;

        public static PotentialUI Instance => instance;

        [SerializeField] private TextMeshProUGUI remainPoints;

        public TextMeshProUGUI RemainPoints => remainPoints;

        [SerializeField] private TextMeshProUGUI powerPoints;

        public TextMeshProUGUI PowerPoints => powerPoints;

        [SerializeField] private TextMeshProUGUI magicPoints;

        public TextMeshProUGUI MagicPoints => magicPoints;

        [SerializeField] private TextMeshProUGUI defensePoints;

        public TextMeshProUGUI DefensePoints => defensePoints;

        [SerializeField] private TextMeshProUGUI strengthPoints;

        public TextMeshProUGUI StrengthPoints => strengthPoints;

        [SerializeField] private TextMeshProUGUI dexterityPoints;

        public TextMeshProUGUI DexterityPoints => dexterityPoints;

        private PlayerStats playerStats;

        protected override void Awake()
        {
            base.Awake();
            if(instance != null) Debug.LogError("Only 1 PotentialUI is allowed to exist!");

            instance = this;
        }

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadRemainPoints();
            LoadCurrentPoints();
        }

        protected override void Start()
        {
            base.Start();
            playerStats = Game.Instance.Player.Stats;
            remainPoints.text = playerStats.RemainPoints + "";
            powerPoints.text = playerStats.Power + "";
            magicPoints.text = playerStats.Magic + "";
            defensePoints.text = playerStats.Defense + "";
            strengthPoints.text = playerStats.Strength + "";
            dexterityPoints.text = playerStats.Dexterity + "";
        }

        private void LoadRemainPoints()
        {
            if(remainPoints != null) return;

            remainPoints = transform.Find("RemainPoints").GetComponentInChildren<TextMeshProUGUI>();
        }

        private void LoadCurrentPoints()
        {
            if(powerPoints != null && magicPoints != null && defensePoints != null && strengthPoints != null && dexterityPoints != null) return;

            powerPoints = transform.Find("CurrentPoints").Find("Power").Find("Points").GetComponent<TextMeshProUGUI>();
            magicPoints = transform.Find("CurrentPoints").Find("Magic").Find("Points").GetComponent<TextMeshProUGUI>();
            defensePoints = transform.Find("CurrentPoints").Find("Defense").Find("Points").GetComponent<TextMeshProUGUI>();
            strengthPoints = transform.Find("CurrentPoints").Find("Strength").Find("Points").GetComponent<TextMeshProUGUI>();
            dexterityPoints = transform.Find("CurrentPoints").Find("Dexterity").Find("Points").GetComponent<TextMeshProUGUI>();
        }
    }
}