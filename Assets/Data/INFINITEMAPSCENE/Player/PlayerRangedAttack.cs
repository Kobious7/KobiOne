using UnityEngine;

namespace InfiniteMap
{
    public class PlayerRangedAttack : PlayerAb
    {
        [SerializeField] private float range = 1;
        [SerializeField] private float timer = 1;
        [SerializeField] private float counter = 0;
        [SerializeField] private bool canAttack;

        private void Update()
        {
            if(Player.IsUIOpening) return;
            
            CountTime();

            if (InputManager.Instance.Fire1 > 0 && canAttack)
            {
                Attack();
            }
        }

        private void CountTime()
        {
            if (counter >= timer)
            {
                canAttack = true;
                return;
            }

            counter += Time.deltaTime;
        }

        private void Attack()
        {
            canAttack = false;
            counter = 0;

            Player.Anim.RangedAttack();
        }
    }
}