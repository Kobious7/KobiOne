namespace Battle
{
    public class PlayerStats : EntityStats
    {
        protected override void Start()
        {
            base.Start();
            
            PlayerInfo playerInfo = Game.Instance.MapData.PlayerInfo;

            if (Game.Instance.MapData.MapCanLoad)
            {
                attack = playerInfo.Attack;
                magicAttack = playerInfo.MagicAttack;
                maxHP = playerInfo.HP;
                currentHP = maxHP;
                swordrainDamage = playerInfo.SwordrainDamage;
                slashDamage = playerInfo.SlashDamage;
                defense = playerInfo.Defense;
                accuracy = playerInfo.Accuracy;
                damageRange = playerInfo.DamageRange;
                critRate = playerInfo.CritRate;
                critDamage = playerInfo.CritDamage;
                manaRegen = playerInfo.ManaRegen;
            }

            InitBuff(); 
        }
    }
}