using UnityEngine;

namespace InfiniteMap
{
    public class PlayerMovement : PlayerAb
    {
        [SerializeField] private float speed = 5;
        [SerializeField] private float jumpForce = 10;

        private void Update()
        {
            if(Player.IsUIOpening) return;

            if (InputManager.Instance.Horizontal == 0) Player.Anim.IdleAnim();
            else Move();
            Jump();
        }

        public void Move()
        {
            Player.Rb2D.velocity = new Vector2(InputManager.Instance.Horizontal * speed, Player.Rb2D.velocity.y);
            Vector2 distanceMoved2D = Player.Rb2D.velocity * Time.deltaTime;
            Vector3 distanceMoved = new Vector3(distanceMoved2D.x, distanceMoved2D.y, 0);

            Player.Anim.RunAnim();

            if (InputManager.Instance.Horizontal > 0)
            {
                Game.Instance.Map.Distance += distanceMoved2D.magnitude;
                Player.Model.localScale = new Vector3(1, 1, 1);
            }

            if (InputManager.Instance.Horizontal < 0)
            {
                Game.Instance.Map.Distance -= distanceMoved2D.magnitude;
                if (Game.Instance.Map.Distance < 0) Game.Instance.Map.Distance = 0;
                Player.Model.localScale = new Vector3(-1, 1, 1);
            }
        }

        public void Jump()
        {
            if (!InputManager.Instance.Jump) return;

            Player.Rb2D.velocity = new Vector2(Player.Rb2D.velocity.x, jumpForce);
        }
    }
}