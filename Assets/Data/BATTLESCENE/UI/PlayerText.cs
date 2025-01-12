using UnityEngine;

namespace Battle
{
    public class PlayerText : EntityText
    {
        private static PlayerText instance;

        public static PlayerText Instance => instance;

        protected override void Awake()
        {
            base.Awake();
            if (instance != null) Debug.LogError("Only 1 PlayerText exists!");

            instance = this;
        }
    }
}