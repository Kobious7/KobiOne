using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Battle
{
    public class BattleResult : GMono
    {
        private static BattleResult instance;

        public static BattleResult Instance => instance;

        [SerializeField] private Image win;

        public Image Win => win;

        [SerializeField] private Image lose;

        public Image Lose => lose;

        [SerializeField] private Image opacityBG;

        public Image OpacityBG => opacityBG;

        [SerializeField] private Transform loadMap;

        public Transform LoadMap => loadMap;

        protected override void Awake()
        {
            base.Awake();
            if (instance != null) Debug.LogError("1");

            instance = this;
        }

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadImages();
            LoadMapLoad();
        }

        public void LoadImages()
        {
            if (win != null && lose != null && opacityBG != null) return;

            win = transform.Find("Win").GetComponent<Image>();
            lose = transform.Find("Lose").GetComponent<Image>();
            opacityBG = transform.Find("OpacityBG").GetComponent<Image>();
        }

        private void LoadMapLoad()
        {
            if (loadMap != null) return;

            loadMap = transform.Find("LoadMap");
        }
    }
}