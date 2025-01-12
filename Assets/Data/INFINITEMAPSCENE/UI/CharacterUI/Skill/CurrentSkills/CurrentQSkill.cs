using UnityEngine;

namespace InfiniteMap
{
    public class CurrentQSkill : CurrentSkill
    {
        private static CurrentQSkill instance;

        public static CurrentQSkill Instance => instance;

        protected override void Awake()
        {
            base.Awake();
            if(instance != null) Debug.LogError("Only 1 CurrentQSkill is allowed to exist!");

            instance = this;
        }
    }
}