using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smooth_speed = 0.125f;
    public Vector3 offset;

    private void Start()
    {
        transform.position = target.position + offset;
    }

    private void FixedUpdate()
    {
        // Late update so target have already conducted its movement
        Vector3 desired_position = target.position + offset;
        float distance = Vector3.Distance(desired_position, transform.position);
        Vector3 smoothed_position = Vector3.Lerp(transform.position, desired_position, smooth_speed);
        transform.position = smoothed_position;

        transform.LookAt(target);
    }
}
