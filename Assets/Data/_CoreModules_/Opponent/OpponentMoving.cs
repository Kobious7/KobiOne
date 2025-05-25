using System.Collections;
using UnityEngine;
public class OpponentMoving : OpponentComponent
{
    [SerializeField] private Transform targetPos;
    [SerializeField] private Vector3 firstPos;
    [SerializeField] private Vector3 lastPos;
    [SerializeField] private float speed;
    [SerializeField] public bool testMoveToTarget;
    [SerializeField] public bool moving;
    [SerializeField] public bool moveBack;
    [SerializeField] protected float targetRadius;
    [SerializeField] public bool ismove;

    protected override void Start()
    {
        base.Start();
        GetTargetRadius();
    }

    // protected void Update()
    // {
    //     if(testMoveToTarget && !moving) 
    //     {
    //         Debug.Log("Move");

    //         Move();

    //         moving = true;
    //     }

    //     if(moveBack && !ismove)
    //     {
    //         StartCoroutine(MoveBack());

    //         ismove = true;
    //     }
    // }

    private void Move()
    {
        StartCoroutine(MoveToTarget());
    }

    public IEnumerator MoveToTarget()
    {
        firstPos = transform.parent.position;

        Opponent.Anim.RunAnim();

        while (Vector3.Distance(transform.parent.position, targetPos.position) > targetRadius)
        {
            transform.parent.position = Vector3.MoveTowards(transform.parent.position, targetPos.position, speed * Time.deltaTime);

            yield return null;
        }
    }

    public IEnumerator MoveBack()
    {
        Opponent.Anim.MoveB = true;
        Opponent.Anim.RunAnim();

        while (Vector3.Distance(transform.parent.position, firstPos) > Time.deltaTime)
        {
            transform.parent.position = Vector3.MoveTowards(transform.parent.position, firstPos, speed * Time.deltaTime);

            yield return null;
        }

        transform.parent.position = firstPos;
        Opponent.Model.localScale = Vector3.one;
        Opponent.Anim.MoveB = false;
    }

    protected virtual void GetTargetRadius()
    {
        targetRadius = Game.Instance.Player.BattleCC.radius;   
    }
}