using System.Collections;
using UnityEngine;

public class RotateAroundPoint : MonoBehaviour
{
    [SerializeField] private Transform center;
    [SerializeField] private Transform pointToRotate;
    [SerializeField] private Transform trail;
    [SerializeField] private float targetAngle = 60f;
    [SerializeField] private float duration;
    private float speed;
    private bool isRotating = false;
    private Vector3 originalPointPosition;
    private Quaternion originalPointRotation;

    private void Start()
    {
        originalPointPosition = pointToRotate.localPosition;
        originalPointRotation = pointToRotate.localRotation;
        trail.position = pointToRotate.position;
        speed = Mathf.Abs(targetAngle) / duration;      
    }

    private void Update()
    {
        if (trail.gameObject.activeSelf)
        {
            trail.position = pointToRotate.position;
        }
    }

    public void StartRotation()
    {
        if (!isRotating)
            StartCoroutine(RotateByAngle(targetAngle));
    }

    private IEnumerator RotateByAngle(float angle)
    {
        isRotating = true;

        float rotated = 0f;
        float direction = Mathf.Sign(angle);

        while (Mathf.Abs(rotated) < Mathf.Abs(angle))
        {
            float step = speed * Time.deltaTime;
            if (Mathf.Abs(rotated + step) > Mathf.Abs(angle))
                step = Mathf.Abs(angle) - Mathf.Abs(rotated);

            if(transform.parent.parent.parent.parent.localScale.x == 1)
            {
                pointToRotate.RotateAround(center.position, Vector3.forward, step * direction);  
            }

            if(transform.parent.parent.parent.parent.localScale.x == -1)
            {
                pointToRotate.RotateAround(center.position, Vector3.back, step * direction);  
            }
            rotated += step;

            yield return null;
        }

        isRotating = false;
    }

    public void ResetRotation()
    {
        pointToRotate.localPosition = originalPointPosition;
        pointToRotate.localRotation = originalPointRotation;
        trail.position = pointToRotate.position;
    }
}
