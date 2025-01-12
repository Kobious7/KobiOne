using UnityEngine;

namespace InfiniteMap
{
    public class CurrentESkill : CurrentSkill
    {
        private static CurrentESkill instance;

        public static CurrentESkill Instance => instance;

        protected override void Awake()
        {
            base.Awake();
            if(instance != null) Debug.LogError("Only 1 CurrentESkill is allowed to exist!");

            instance = this;
        }
    }
}