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
            CountTime();

            if (InputManager.Instance.Fire1 > 0 && canAttack)
            {
                StartCoroutine(Attack());
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

        private IEnumerator Attack()
        {
            canAttack = false;
            counter = 0;
            Player.Anim.MeleeAttack();

            yield return StartCoroutine(Player.Anim.WaitAnim("SwordMeleeAttack"));
            CheckHit();
        }

        private void CheckHit()
        {
            Collider[] monsters = Physics.OverlapSphere(Player.CenterPoint.transform.position, range, layerMask);

            foreach (Collider monster in monsters)
            {
                Debug.Log("" + monster.transform.name);
                LoadScene("Battle");
                return;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawSphere(Player.CenterPoint.transform.position, range);
        }
    }
}