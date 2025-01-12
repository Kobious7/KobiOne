using UnityEngine;

namespace InfiniteMap
{
    public class MonsterCollision : MonsterAb
    {
        private void Update()
        {
            transform.position = transform.parent.position;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.name == "Player")
            {
                Debug.Log("Collision");
            }
        }
    }
}