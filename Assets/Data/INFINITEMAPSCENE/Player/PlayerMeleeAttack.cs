using System.Collections;
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
            Player.Anim.MeleeAttack();
        }

        public IEnumerator CheckHit()
        {
            Debug.Log("a");
            Collider[] monsters = Physics.OverlapSphere(Player.CenterPoint.transform.position, range, layerMask);

            if (monsters.Length > 0)
            {
                Debug.Log(">0");
                PlayerEventsInAnim.Instance.Hit = true;
            }

            foreach (Collider monster in monsters)
            {
                Debug.Log("" + monster.transform.name);

                yield return new WaitForSeconds(1.5f);

                LoadScene("Battle");
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawSphere(Player.CenterPoint.transform.position, range);
        }
    }
}