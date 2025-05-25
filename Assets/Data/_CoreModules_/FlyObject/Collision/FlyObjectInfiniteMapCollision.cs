using UnityEngine;

public class FlyObjectInfiniteMapCollision : FlyObjectCollision
{
    protected override void Collide(Collider other)
    {
        base.Collide(other);
        
        Game.Instance.FlyObjectSpawner.Despawn(transform.parent);

        if (other.transform.parent.name == "Monster")
        {
            Game.Instance.LoadAllObj(other.transform);
        }
    }
}