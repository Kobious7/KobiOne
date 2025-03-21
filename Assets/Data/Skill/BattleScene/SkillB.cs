using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class SkillB : GMono
    {
        private static SkillB instance;

        public static SkillB Instance => instance;

        [SerializeField] private SkillNode qSkill;

        public SkillNode QSkill => qSkill;

        [SerializeField] private SkillNode eSkill;

        public SkillNode ESkill => eSkill;

        [SerializeField] private SkillNode spaceSkill;

        public SkillNode SpaceSkill => spaceSkill;

        [SerializeField] private bool qUnlocking;

        public bool QUnlocking
        {
            get => qUnlocking;
            set => qUnlocking = value;
        }

        [SerializeField] private bool eUnlocking;

        public bool EUnlocking
        {
            get => eUnlocking;
            set => eUnlocking = value;
        }

        [SerializeField] private bool spaceUnlocking;

        public bool SpaceUnlocking
        {
            get => spaceUnlocking;
            set => spaceUnlocking = value;
        }

        [SerializeField] private Q q;

        public Q Q => q;

        [SerializeField] private SkillBActivating skillActivator;

        public SkillBActivating SkillActivator => skillActivator;

        [SerializeField] private SkillBDamageCalculator calculator;

        public SkillBDamageCalculator Calculator => calculator;

        private PlayerInfo playerInfo;

        protected override void Awake()
        {
            base.Awake();
            if(instance != null) Debug.LogError("Only 1 SkillB is allowed to exist");

            instance = this;
        }

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadQ();
            LoadActivator();
            LoadCalculator();
        }

        protected override void Start()
        {
            base.Start();

            playerInfo = Game.Instance.MapData.PlayerInfo;

            LoadSkillFromSO();
            DestructiveObjectSpawner.Instance.LoadPrefabsResources();
        }

        private void LoadQ()
        {
            if(q != null) return;

            q = GetComponentInChildren<Q>();
        }

        private void LoadActivator()
        {
            if(skillActivator != null) return;

            skillActivator = GetComponentInChildren<SkillBActivating>();
        }

        private void LoadCalculator()
        {
            if(calculator != null) return;

            calculator = GetComponentInChildren<SkillBDamageCalculator>();
        }

        private void LoadSkillFromSO()
        {
            if(!Game.Instance.MapData.MapCanLoad) return;

            if(playerInfo.QSkill != null)
            {
                qSkill.Level = playerInfo.QSkill.Level;
                qSkill.skillSO = playerInfo.QSkill.skillSO;
            }
            if(playerInfo.ESkill != null)
            {
                eSkill.Level = playerInfo.ESkill.Level;
                eSkill.skillSO = playerInfo.ESkill.skillSO;
            }
            if(playerInfo.SpaceSkill != null)
            {
                spaceSkill.Level = playerInfo.SpaceSkill.Level;
                spaceSkill.skillSO = playerInfo.SpaceSkill.skillSO;
            }
        }
    }
}