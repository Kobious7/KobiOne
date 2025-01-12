using UnityEngine;

namespace Battle
{
    public class BotHPUpdate : HPUpdate
    {
        protected override float GetNewFillAmount()
        {
            return (float)Game.Instance.Bot.Stats.CurrentHP / Game.Instance.Bot.Stats.MaxHP;
        }

        protected override string GetNewString()
        {
            return $"{Game.Instance.Bot.Stats.CurrentHP}/{Game.Instance.Bot.Stats.MaxHP}";
        }
    }
}