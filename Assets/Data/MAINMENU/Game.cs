using UnityEngine;

namespace MainMenu
{
    public class Game : GMono
    {
        private static Game instance;

        public static Game Instance => instance;

        protected override void Awake()
        {
            base.Awake();
            if(instance != null) Debug.LogError("Only 1 Game is allowed to exist!");

            instance = this;
        }
    }
}