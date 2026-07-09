using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    public Transform target;

    public Vector3 offset =
        new Vector3(0, 6, -8);

    public float followSpeed = 5f;
    public float rotateSpeed = 5f;

    void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 desiredPosition =
            target.position +
            target.TransformDirection(offset);

        transform.position =
            Vector3.Lerp(
                transform.position,
                desiredPosition,
                followSpeed * Time.deltaTime
            );

        Quaternion desiredRotation =
            Quaternion.LookRotation(
                target.position - transform.position
            );

        transform.rotation =
            Quaternion.Slerp(
                transform.rotation,
                desiredRotation,
                rotateSpeed * Time.deltaTime
            );
    }
}