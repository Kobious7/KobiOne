using UnityEngine;

namespace Battle
{
    public class BotAb : GMono
    {
        [SerializeField] private Bot bot;

        public Bot Bot => bot;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadBot();
        }

        private void LoadBot()
        {
            if (bot != null) return;

            bot = transform.parent.GetComponent<Bot>();
        }
    }
}