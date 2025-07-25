using UnityEngine;

public class IMMonsterMovement : IMEntityMovement
{
    [Header("Move Specs")]
    [SerializeField] private float speed = 2;
    [SerializeField] private float radius = 10;
    [SerializeField] private float xL, xR;
    [SerializeField] private bool isLeft, isRight;
    [SerializeField] private int[] arr = { -1, 1 };
    [SerializeField] private float distance = 10;

    [Header("Idle & Move Time")]
    [SerializeField] private float minIdleTime = 2f;
    [SerializeField] private float maxIdleTime = 10f;
    [SerializeField] private float minMoveDuration = 2f;
    [SerializeField] private float maxMoveDuration = 10f;
    private bool isIdle;
    private float idleTimer;
    private float moveTimer;
    private IMMonster monster;
    private IMMonsterAnim anim;

    protected override void Start()
    {
        base.Start();
        monster = Entity as IMMonster;
        anim = Entity.Anim as IMMonsterAnim;
        xL = transform.parent.position.x - radius;
        xR = transform.parent.position.x + radius;
        int rand = arr[Random.Range(0, arr.Length)];

        if (rand == 1) isRight = true;
        if (rand == -1) isLeft = true;

        ResetMoveTimer();
    }

    private void Update()
    {
        if (monster.IsBeingHit)
        {
            monster.Anim.Idle();
            monster.Model.localScale = monster.Model.localScale.x != -1 ? new Vector3(-1, 1, 1) : monster.Model.localScale;
            return;
        }
        
        if (Vector3.Distance(InfiniteMapManager.Instance.Player.transform.position, transform.parent.position) > distance)
        {
            monster.Animator.speed = 0;
            return;
        }

        monster.Animator.speed = 1;
        
        if (isIdle)
        {
            anim.Idle();

            idleTimer -= Time.deltaTime;
            if (idleTimer <= 0f)
            {
                isIdle = false;
                ResetMoveTimer();
            }
        }
        else
        {
            moveTimer -= Time.deltaTime;
            Move();

            if (moveTimer <= 0f)
            {
                isIdle = true;
                idleTimer = Random.Range(minIdleTime, maxIdleTime);
            }
        }
    }

    protected override void Move()
    {
        anim.Run();

        if (isRight) MoveRight();
        if (isLeft) MoveLeft();
    }

    private void MoveRight()
    {
        monster.Model.localScale = new Vector3(1, 1, 1);

        if (transform.parent.position.x <= xR)
            transform.parent.Translate(Vector3.right * speed * Time.deltaTime);
        else
        {
            isLeft = true;
            isRight = false;
        }
    }

    private void MoveLeft()
    {
        monster.Model.localScale = new Vector3(-1, 1, 1);

        if (transform.parent.position.x >= xL)
            transform.parent.Translate(Vector3.left * speed * Time.deltaTime);
        else
        {
            isLeft = false;
            isRight = true;
        }
    }

    private void ResetMoveTimer()
    {
        moveTimer = Random.Range(minMoveDuration, maxMoveDuration);
    }
}