using UnityEngine;

namespace Battle
{
    public class SwrodrainFlying : SwordrainAb
    {
        [SerializeField] private Transform target;
        [SerializeField] private float speed = 6;
        [SerializeField] private bool bot;
        [SerializeField] private bool player;
        [SerializeField] private float targetRadius;

        private void Update()
        {
            LockTarget();
            Fly();
        }

        public void Fly()
        {
            //Debug.Log(Vector3.Distance(transform.parent.position, target.position));
            targetRadius = player ? Game.Instance.Player.CapCollider.radius : bot ? Game.Instance.Bot.CapCollider.radius : 0;

            if (Vector3.Distance(transform.parent.position, target.position) > targetRadius)
            {
                transform.parent.position = Vector3.Lerp(transform.parent.position, target.position, speed * Time.deltaTime);
            }
        }

        public void LockTarget()
        {
            Vector3 direction = target.position - transform.parent.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.parent.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
        }
    }
}