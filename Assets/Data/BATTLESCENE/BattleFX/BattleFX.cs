using UnityEngine;

namespace Battle
{
    public class BattleFX : GMono
    {
        [SerializeField] private Transform model;

        public Transform Model => model;

        [SerializeField] private Animator animator;

        public Animator Animator => animator;

        [SerializeField] private BattleFXAnim anim;

        public BattleFXAnim Anim => anim;

        [SerializeField] private BattleFXDespawn battleFXDespawn;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadModelAAnimator();
            LoadAnim();
            LoadFXDespawn();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            battleFXDespawn.Despawn = true;
        }

        private void LoadModelAAnimator()
        {
            if (model != null) return;

            model = transform.Find("Model");

            if (animator != null) return;

            animator = model.GetComponent<Animator>();
        }

        private void LoadAnim()
        {
            if (anim != null) return;

            anim = GetComponentInChildren<BattleFXAnim>();
        }

        private void LoadFXDespawn()
        {
            if (battleFXDespawn != null) return;

            battleFXDespawn = GetComponentInChildren<BattleFXDespawn>();
        }
    }
}