using UnityEngine;

public class DestructiveObjectFindTarget : DestructiveObjectAb
{
    [SerializeField] private float speed = 2;

    private void Update()
    {
        if (DObject.Target == null) return;

        LockTarget();
        Fly();
    }

    public void Fly()
    {
        if (Vector3.Distance(transform.parent.position, DObject.Target.position) > Time.deltaTime)
        {
            transform.parent.position = Vector3.Lerp(transform.parent.position, DObject.Target.position, speed * Time.deltaTime);
        }
    }

    public void LockTarget()
    {
        Vector3 direction = DObject.Target.position - transform.parent.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        DObject.Model.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
    }
}