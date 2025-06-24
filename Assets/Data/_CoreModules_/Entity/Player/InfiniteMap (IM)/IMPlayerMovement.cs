using UnityEngine;

public class IMPlayerMovement : IMEntityMovement
{
    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpForce = 10;
    [SerializeField] private bool isGrounded = true;
    [SerializeField] private bool isJumping, isFalling;
    [SerializeField] private LayerMask layerMask;
    private InfiniteMapManager infiniteMapManager;
    private IMPlayer player;
    private IMPlayerAnim anim;

    protected override void Start()
    {
        base.Start();
        infiniteMapManager = InfiniteMapManager.Instance;
        player = Entity as IMPlayer;
        anim = Entity.Anim as IMPlayerAnim;
    }

    private void Update()
    {
        if (player.IsUIOpening) return;

        CheckGrounded();
        HandleJump();

        if (InputManager.Instance.Horizontal == 0 && isGrounded)
        {
            anim.Idle();
        }
        else
        {
            Move();
        }

        Jump();
    }

    private void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(player.RigModel.position, 0.1f, layerMask);
    }

    private void HandleJump()
    {
        if (isJumping && player.Rb2D.velocity.y < 0)
        {
            isJumping = false;
            isFalling = true;
            anim.Fall();
        }

        if (isFalling && isGrounded)
        {
            isFalling = false;
            anim.FallToIlde();
            Debug.Log("FallToIdle");
        }
    }

    protected override void Move()
    {
        player.Rb2D.velocity = new Vector2(InputManager.Instance.Horizontal * speed, player.Rb2D.velocity.y);
        Vector2 distanceMoved2D = player.Rb2D.velocity * Time.deltaTime;
        Vector3 distanceMoved = new Vector3(distanceMoved2D.x, distanceMoved2D.y, 0);

        anim.Run();

        if (InputManager.Instance.Horizontal > 0)
        {
            infiniteMapManager.Map.Distance += distanceMoved2D.magnitude;
            player.Model.localScale = new Vector3(1, 1, 1);
        }

        if (InputManager.Instance.Horizontal < 0)
        {
            infiniteMapManager.Map.Distance -= distanceMoved2D.magnitude;
            if (infiniteMapManager.Map.Distance < 0) infiniteMapManager.Map.Distance = 0;
            player.Model.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void Jump()
    {
        if (!InputManager.Instance.Jump) return;

        player.Rb2D.velocity = new Vector2(player.Rb2D.velocity.x, jumpForce);
        isJumping = true;
        isFalling = false;
        isGrounded = false;

        anim.IdleToJump();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(Entity.Model.position, 0.1f);
    }
}