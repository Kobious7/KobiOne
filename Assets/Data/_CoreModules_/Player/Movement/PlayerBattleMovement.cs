using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleMovement : PlayerComponent
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

    public IEnumerator MoveToTarget()
    {
        firstPos = transform.parent.position;

        Player.Anim.Run();

        while (Vector3.Distance(transform.parent.position, targetPos.position) > targetRadius)
        {
            transform.parent.position = Vector3.MoveTowards(transform.parent.position, targetPos.position, speed * Time.deltaTime);

            yield return null;
        }
    }

    public IEnumerator MoveBack()
    {
        Player.Model.localScale = new Vector3(-1, 1, 1);
        Player.Anim.Run();

        while (Vector3.Distance(transform.parent.position, firstPos) > Time.deltaTime)
        {
            transform.parent.position = Vector3.MoveTowards(transform.parent.position, firstPos, speed * Time.deltaTime);

            yield return null;
        }

        transform.parent.position = firstPos;
        Player.Model.localScale = Vector3.one;
        Player.Anim.Idle();
    }

    protected virtual void GetTargetRadius()
    {
        targetRadius = Game.Instance.Opponent.CapCollider.radius;     
    }
}