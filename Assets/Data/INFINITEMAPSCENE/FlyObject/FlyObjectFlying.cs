using UnityEngine;

namespace InfiniteMap
{
    public class FlyObjectFlying : FlyObjectAb
    {
        [SerializeField] private float speed = 2;

        private void Update()
        {
            Fly();
        }

        private void Fly()
        {
            transform.parent.Translate(Vector3.right.normalized * speed * Time.deltaTime);
        }
    }
}