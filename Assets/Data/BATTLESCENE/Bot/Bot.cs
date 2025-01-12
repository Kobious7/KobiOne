using UnityEngine;

namespace Battle
{
    public class Bot : Entity
    {
        [SerializeField] private BotPick botPick;

        public BotPick BotPick => botPick;

        [SerializeField] private BotMoveTile botMoveTile;

        public BotMoveTile BotMoveTile => botMoveTile;

        [SerializeField] private BotDestroyAndFill botDestroyAndFill;

        public BotDestroyAndFill BotDestroyAndFill => botDestroyAndFill;

        protected override void Start()
        {
            base.Start();
            if (!Game.Instance.MapData.MapCanLoad) return;
            Stats.MaxHP = Game.Instance.MapData.MonsterInfo.HP;
            Stats.SwordrainDamage = Game.Instance.MapData.MonsterInfo.SwordrainDamage;
            Stats.SlashDamage = Game.Instance.MapData.MonsterInfo.SlashDamage;
            Stats.CurrentHP = Stats.MaxHP;
        }

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadBotPick();
            LoadBotMoveTile();
            LoadBotDestroyAndFill();
        }

        private void LoadBotPick()
        {
            if (botPick != null) return;

            botPick = GetComponentInChildren<BotPick>();
        }

        private void LoadBotMoveTile()
        {
            if (botMoveTile != null) return;

            botMoveTile = GetComponentInChildren<BotMoveTile>();
        }

        private void LoadBotDestroyAndFill()
        {
            if (botDestroyAndFill != null) return;

            botDestroyAndFill = GetComponentInChildren<BotDestroyAndFill>();
        }
    }
}