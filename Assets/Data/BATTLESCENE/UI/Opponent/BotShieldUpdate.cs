using UnityEngine;

namespace Battle
{
    public class BotShieldUpdate : ShieldUpdate
    {
        protected override string GetNewString()
        {
            return $"{Game.Instance.Bot.Stats.ShieldStack}";
        }
    }
}