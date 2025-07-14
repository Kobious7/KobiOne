using System.Xml.Serialization;
using UnityEngine;

public class SwrodrainFlying : SwordrainAb
{
    [SerializeField] protected Transform target;
    [SerializeField] private float speed = 6;
    [SerializeField] private bool bot;
    [SerializeField] private bool player;
    [SerializeField] private float targetRadius;

    protected override void Start()
    {
        base.Start();
        GetTarget();
    }

    private void Update()
    {
        LockTarget();
        Fly();
    }

    public void Fly()
    {
        //Debug.Log(Vector3.Distance(transform.parent.position, target.position));
        targetRadius = player ? BattleManager.Instance.Player.CapsuleCollider.radius : bot ? BattleManager.Instance.Monster.CapsuleCollider.radius : 0;

        if (Vector3.Distance(transform.parent.position, target.position) > targetRadius)
        {
            transform.parent.position = Vector3.Lerp(transform.parent.position, target.position, speed * Time.deltaTime);
        }
    }

    public void LockTarget()
    {
        Vector3 direction = target.position - transform.parent.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.parent.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
    }

    protected virtual void GetTarget() {}
}