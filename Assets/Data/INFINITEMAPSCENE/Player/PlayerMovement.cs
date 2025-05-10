using UnityEngine;

namespace InfiniteMap
{
    public class PlayerMovement : PlayerAb
    {
        [SerializeField] private float speed = 5;
        [SerializeField] private float jumpForce = 10;
        [SerializeField] private bool isGrounded = true;
        [SerializeField] private bool isJumping, isFalling;
        [SerializeField] private LayerMask layerMask;

        private void Update()
        {
            if(Player.IsUIOpening) return;

            CheckGrounded();
            HandleJump();

            if (InputManager.Instance.Horizontal == 0 && isGrounded)
            {
                Player.Anim.Idle();
            }
            else
            {
                Move();
            }

            Jump();
        }

        private void CheckGrounded()
        {
           isGrounded = Physics2D.OverlapCircle(Player.RigModel.position, 0.1f, layerMask);
        }

        private void HandleJump()
        {
            if (isJumping && Player.Rb2D.velocity.y < 0)
            {
                isJumping = false;
                isFalling = true;
                Player.Anim.Fall();
            }

            if (isFalling && isGrounded)
            {
                isFalling = false;
                Player.Anim.FallToIlde();
                Debug.Log("FallToIdle");
            }
        }

        public void Move()
        {
            Player.Rb2D.velocity = new Vector2(InputManager.Instance.Horizontal * speed, Player.Rb2D.velocity.y);
            Vector2 distanceMoved2D = Player.Rb2D.velocity * Time.deltaTime;
            Vector3 distanceMoved = new Vector3(distanceMoved2D.x, distanceMoved2D.y, 0);

            Player.Anim.Run();

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
            isJumping = true;
            isFalling = false;
            isGrounded = false;

            Player.Anim.IdleToJump();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(Player.RigModel.position, 0.1f);
        }
    }
}