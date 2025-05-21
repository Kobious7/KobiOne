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

        protected override void Start()
        {
            base.Start();
            xL = transform.parent.position.x - radius;
            xR = transform.parent.position.x + radius;
            int rand = arr[Random.Range(0, arr.Length)];

            if (rand == 1) isRight = true;
            if (rand == -1) isLeft = true;
        }

        private void Update()
        {
            if (Vector3.Distance(Game.Instance.Player.transform.position, transform.parent.position) > distance) return;
            Move();
        }

        private void Move()
        {
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
    }
}