using UnityEngine;

namespace Battle
{
    public class TileBorder : GMono
    {
        [SerializeField] private Transform model;

        public Transform Model => model;

        [SerializeField] private Transform arrows;

        public Transform Arrows => arrows;

        [SerializeField] private bool isDisplayed;

        public bool IsDisplayed
        {
            get => isDisplayed;
            set => isDisplayed = value;
        }

        [SerializeField] private bool isEnter;

        public bool IsEnter
        {
            get => isEnter;
            set => isEnter = value;
        }

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadModel();
        }

        private void LoadModel()
        {
            if (model != null && arrows != null) return;

            model = transform.Find("Model");
            arrows = transform.Find("Arrows");
        }
    }
}