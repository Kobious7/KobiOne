using UnityEngine;

namespace InfiniteMap
{
    public class ParalaxEffect : GMono
    {
        [SerializeField] private float parallaxSpeed, length;
        [SerializeField] private Transform cam;
        [SerializeField] private float startX;

        protected override void Start()
        {
            startX = transform.position.x;
            length = GetComponent<SpriteRenderer>().bounds.size.x;

            Debug.Log("" + length);
        }

        private void FixedUpdate()
        {
            float distance = cam.position.x * parallaxSpeed;
            Debug.Log($"{distance} {startX}");

            transform.position = new Vector3(startX + distance, transform.position.y, transform.position.z);
        }
    }

}