using UnityEngine;

public class SelfRotator : GMono
{
    public float rotationSpeed = -72f; // độ/quay mỗi giây

    void Update()
    {
        RotateSelf(rotationSpeed);
    }

    void RotateSelf(float speed)
    {
        transform.Rotate(Vector3.forward * speed * Time.deltaTime); 
        // Vector3.forward = (0, 0, 1), tức là quay quanh trục Z
    }
}