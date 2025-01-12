using UnityEngine;

namespace Battle
{
    public class BotVHPUpdate : VHPUpdate
    {
        protected override float GetNewFillAmount()
        {
            return (float)Game.Instance.Bot.Stats.VHP / Game.Instance.Bot.Stats.MaxHP * 2;
        }
    }
}