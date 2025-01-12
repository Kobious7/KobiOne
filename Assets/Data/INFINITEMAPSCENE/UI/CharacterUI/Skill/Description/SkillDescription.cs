using TMPro;
using UnityEngine;

namespace InfiniteMap
{
    public class SkillDescription : GMono
    {
        private static SkillDescription instance;

        public static SkillDescription Instance => instance;

        [SerializeField] private TextMeshProUGUI descritionTMP;

        public TextMeshProUGUI DescritionTMP => descritionTMP;

        protected override void Awake()
        {
            base.Awake();
            if (instance != null) Debug.LogError("Only 1 SkillDescription is allowed to exist!");

            instance = this;
        }

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadTMP();
        }

        private void LoadTMP()
        {
            if (descritionTMP != null) return;

            descritionTMP = GetComponent<TextMeshProUGUI>();
        }
    }
}
