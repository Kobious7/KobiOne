using UnityEngine;

namespace Battle
{
    public class PlayerHPUpdate : HPUpdate
    {
        protected override float GetNewFillAmount()
        {
            return (float)Game.Instance.Player.Stats.CurrentHP / Game.Instance.Player.Stats.MaxHP;
        }

        protected override string GetNewString()
        {
            return $"{Game.Instance.Player.Stats.CurrentHP}/{Game.Instance.Player.Stats.MaxHP}";
        }
    }
}