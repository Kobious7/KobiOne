using System.Collections;
using UnityEngine;

public class FlyObjectCollision : FlyObjectAb
{
    private void OnTriggerEnter(Collider other)
    {
        Collide(other);
    }

    protected virtual void Collide(Collider other)
    {
        //Override
    }
}