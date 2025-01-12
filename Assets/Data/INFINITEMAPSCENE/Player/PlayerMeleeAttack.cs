using UnityEngine;

namespace InfiniteMap
{
    public class PlayerMeleeAttack : PlayerAb
    {
        [SerializeField] private float range = 1;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float timer = 1;
        [SerializeField] private float counter = 0;
        [SerializeField] private bool canAttack;

        private void Update()
        {
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
            Player.Anim.MeleeAttack();
            CheckHit();
        }

        private void CheckHit()
        {
            Collider[] monsters = Physics.OverlapSphere(Player.AttackPoint.position, range, layerMask);

            foreach (Collider monster in monsters)
            {
                Debug.Log("" + monster.transform.name);
                LoadScene("Battle");
                return;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawSphere(Player.AttackPoint.position, range);
        }
    }
}