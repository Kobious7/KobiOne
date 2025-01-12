using UnityEngine;

namespace Battle
{
    public class ShieldUpdate : ShieldAb
    {
        private void Update()
        {
            UpdateShield();
        }

        private void UpdateShield()
        {
            Shield.StackText.text = GetNewString();
        }

        protected virtual string GetNewString()
        {
            return null;
        }
    }
}