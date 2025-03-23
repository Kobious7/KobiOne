namespace Battle
{
    public class BotStats : EntityStats
    {
        protected override void Start()
        {
            base.Start();

            InitBuff();
        }
    }
}