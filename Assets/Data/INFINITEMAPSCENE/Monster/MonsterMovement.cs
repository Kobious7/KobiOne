using UnityEngine;

namespace InfiniteMap
{
    public class MonsterMovement : MonsterAb
    {
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

        protected override void Start()
        {
            base.Start();
            xL = transform.parent.position.x - radius;
            xR = transform.parent.position.x + radius;
            int rand = arr[Random.Range(0, arr.Length)];

            if (rand == 1) isRight = true;
            if (rand == -1) isLeft = true;

            ResetMoveTimer();
        }

        private void Update()
        {
            if (Vector3.Distance(Game.Instance.Player.transform.position, transform.parent.position) > distance)
            {
                Monster.Animator.speed = 0;
                return;
            }

            Monster.Animator.speed = 1;
            
            if (isIdle)
            {
                Monster.Anim.Idle();

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

        private void Move()
        {
            Monster.Anim.Run();

            if (isRight) MoveRight();
            if (isLeft) MoveLeft();
        }

        private void MoveRight()
        {
            Monster.Model.localScale = new Vector3(1, 1, 1);

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
            Monster.Model.localScale = new Vector3(-1, 1, 1);

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
}