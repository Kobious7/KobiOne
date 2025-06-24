using UnityEngine;

public class Fly : MonoBehaviour
{
    public Transform model; // bạn đang gán Sword vào đây rồi
    public float speed = 5f;

    void Update()
    {
        // Di chuyển sang phải theo local hoặc world space
        transform.position += Vector3.right * speed * Time.deltaTime;
    }
}