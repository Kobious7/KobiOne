using UnityEngine;

namespace InfiniteMap
{
    public class CurrentSpaceSkill : CurrentSkill
    {
        private static CurrentSpaceSkill instance;

        public static CurrentSpaceSkill Instance => instance;

        protected override void Awake()
        {
            base.Awake();
            if(instance != null) Debug.LogError("Only 1 CurrentSpaceSkill is allowed to exist!");

            instance = this;
        }
    }
}