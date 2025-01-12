using UnityEngine;

namespace Battle
{
    public class PlayerMoving : EntityMoving
    {
        protected override void GetTargetRadius()
        {
            targetRadius = Game.Instance.Bot.CapCollider.radius;
        }
    }
}