using UnityEngine;

namespace Battle
{
    public class PlayerVHPUpdate : VHPUpdate
    {
        protected override float GetNewFillAmount()
        {
            return (float)Game.Instance.Player.Stats.VHP / Game.Instance.Player.Stats.MaxHP * 2;
        }
    }
}