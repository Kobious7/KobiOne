using UnityEngine;

namespace Battle
{
    public class BotMPUpdate : MPUpdate
    {
        protected override float GetNewFillAmount()
        {
            return (float)Game.Instance.Bot.Stats.Mana / 100;
        }

        protected override string GetNewString()
        {
            return $"{Game.Instance.Bot.Stats.Mana}%";
        }
    }
}