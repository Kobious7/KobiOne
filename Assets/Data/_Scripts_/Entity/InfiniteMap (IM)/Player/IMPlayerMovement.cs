using System.Collections;
using UnityEngine;

public class IMPlayerMovement : IMEntityMovement
{
    [SerializeField] private float jumpForce = 10;
    [SerializeField] private bool isGrounded = true;
    [SerializeField] private bool isJumping, isFalling;
    [SerializeField] private int maxJumpCount = 2;
    [SerializeField] private int jumpCount;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float xStop;

    public float XStop => xStop;
    
    private InfiniteMapManager infiniteMapManager;
    private IMPlayer player;
    private IMPlayerAnim anim;

    protected override void Start()
    {
        base.Start();
        infiniteMapManager = InfiniteMapManager.Instance;
        player = Entity as IMPlayer;
        anim = Entity.Anim as IMPlayerAnim;
        infiniteMapManager.Map.MapSwap.OnMapChange += CalculateXStop;
    }

    private void Update()
    {
        if (player.CanLockMovement)
        {
            player.Model.localScale = player.Model.localScale.x != 1 ? Vector3.one : player.Model.localScale;
            return;
        }

        if (InfiniteMapManager.Instance.IsUIOpening) return;

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
        bool wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(player.RigModel.position, 0.1f, layerMask);

        if (!isJumping && isGrounded && !wasGrounded)
        {
            jumpCount = 0;
        }
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
        }
    }

    protected override void Move()
    {
        Vector2 distanceMoved2D = player.Rb2D.velocity * Time.deltaTime;

        anim.Run();

        if (InputManager.Instance.Horizontal > 0)
        {
            if (transform.parent.position.x >= 0)
                infiniteMapManager.Map.Distance += distanceMoved2D.magnitude;

            player.Model.localScale = new Vector3(1, 1, 1);
        }

        if (InputManager.Instance.Horizontal < 0)
        {
            infiniteMapManager.Map.Distance -= distanceMoved2D.magnitude;
            if (infiniteMapManager.Map.Distance < 0) infiniteMapManager.Map.Distance = 0;
            player.Model.localScale = new Vector3(-1, 1, 1);
        }

        if (infiniteMapManager.Map.Distance <= 0 && InputManager.Instance.Horizontal < 0 && transform.parent.position.x <= xStop)
        {
            player.Rb2D.velocity = new Vector2(0, player.Rb2D.velocity.y);
            return;
        }

        player.Rb2D.velocity = new Vector2(InputManager.Instance.Horizontal * player.StatsSystem.Stats[9].Value, player.Rb2D.velocity.y);
        Vector3 distanceMoved = new Vector3(distanceMoved2D.x, distanceMoved2D.y, 0);
    }

    public void Jump()
    {
        if (!InputManager.Instance.Jump || jumpCount >= maxJumpCount) return;

        ++jumpCount;
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

    private void CalculateXStop(MapEnum current)
    {
        StartCoroutine(CalculateXStopCoroutine(current));
    }

    private IEnumerator CalculateXStopCoroutine(MapEnum current)
    {
        yield return new WaitForSeconds(0.5f);

        Vector3 xStopTemp = new();

        if (infiniteMapManager.Map.Distance <= 500f)
        {
            if (current == MapEnum.Map0)
            {
                xStopTemp = infiniteMapManager.Map.Maps[0].TransformPoint(Vector3.zero);
                xStop = xStopTemp.x - 278.5f;
            }
            else
            {
                xStopTemp = infiniteMapManager.Map.Maps[1].TransformPoint(Vector3.zero);
                xStop = xStopTemp.x - 278.5f;
            }
        }
    }
}