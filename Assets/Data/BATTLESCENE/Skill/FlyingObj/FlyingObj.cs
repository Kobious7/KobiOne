using UnityEngine;

namespace Battle
{
    public class FlyingObj : GMono
    {
        [SerializeField] private Transform model;

        public Transform Model => model;

        [SerializeField] private Transform target;

        public Transform Target
        {
            get => target;
            set => target = value;
        }

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadModel();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            target = null;
        }

        private void LoadModel()
        {
            if (model != null) return;

            model = transform.Find("Model");
        }
    }
}