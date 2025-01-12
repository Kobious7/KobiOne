using UnityEngine;

namespace Battle
{
    public class PlayerMPUpdate : MPUpdate
    {
        protected override float GetNewFillAmount()
        {
            return (float)Game.Instance.Player.Stats.Mana / 100;
        }

        protected override string GetNewString()
        {
            return $"{Game.Instance.Player.Stats.Mana}%";
        }
    }
}