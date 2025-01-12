using UnityEngine;

namespace InfiniteMap
{
    public class PlayerMovement : PlayerAb
    {
        [SerializeField] private float speed = 5;
        [SerializeField] private float jumpForce = 10;

        private void Update()
        {
            if (InputManager.Instance.Horizontal == 0) Player.Anim.IdleAnim();
            else Move();
            Jump();
        }

        public void Move()
        {
            Player.Rb.velocity = new Vector3(InputManager.Instance.Horizontal * speed, Player.Rb.velocity.y, 0);
            Vector3 distanceMoved = Player.Rb.velocity * Time.deltaTime;

            Player.Anim.RunAnim();

            if (InputManager.Instance.Horizontal > 0)
            {
                Game.Instance.Map.Distance += distanceMoved.magnitude;
                Player.Model.localScale = new Vector3(1, 1, 1);
            }

            if (InputManager.Instance.Horizontal < 0)
            {
                Game.Instance.Map.Distance -= distanceMoved.magnitude;
                if (Game.Instance.Map.Distance < 0) Game.Instance.Map.Distance = 0;
                Player.Model.localScale = new Vector3(-1, 1, 1);
            }
        }

        public void Jump()
        {
            if (!InputManager.Instance.Jump) return;

            Player.Rb.velocity = new Vector3(Player.Rb.velocity.x, jumpForce, 0);
        }
    }
}