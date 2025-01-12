using UnityEngine;
using UnityEngine.UI;

namespace Battle
{
    public class SkillUILock : SkillUIAb
    {
        [SerializeField] protected int consume;

        public int Consume => consume;

        [SerializeField] private bool isLock;

        public bool IsLock => isLock;

        private void Update()
        {
            if (Game.Instance.Player.Stats.Mana < consume)
            {
                Skill.Btn.interactable = false;
                ColorBlock colorBlock = Skill.Btn.colors;
                colorBlock.normalColor = Color.black;
                Skill.Btn.colors = colorBlock;
                isLock = true;
            }
            else
            {
                Skill.Btn.interactable = true;
                ColorBlock colorBlock = Skill.Btn.colors;
                colorBlock.normalColor = Color.white;
                Skill.Btn.colors = colorBlock;
                isLock = false;
            }
        }

        protected override void Start()
        {
            base.Start();
            GetConsume();
        }

        protected virtual void GetConsume()
        {
            //Override
        }
    }
}