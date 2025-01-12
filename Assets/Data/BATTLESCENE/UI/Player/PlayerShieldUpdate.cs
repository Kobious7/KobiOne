using UnityEngine;

namespace Battle
{
    public class PlayerShieldUpdate : ShieldUpdate
    {
        protected override string GetNewString()
        {
            return $"{Game.Instance.Player.Stats.ShieldStack}";
        }
    }
}