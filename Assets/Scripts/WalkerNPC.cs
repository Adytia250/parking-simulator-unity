using UnityEngine;

public class WalkerNPC : MonoBehaviour
{
    [Header("Waypoints")]
    public Transform pointA;
    public Transform pointB;

    [Header("Settings")]
    public float moveSpeed = 2f;
    public float waitTime = 1f;

    private Vector3 targetPoint;
    private bool isWaiting = false;

    void Start()
    {
        if (pointA == null || pointB == null)
        {
            Debug.LogError("❌ PointA/PointB belum di-assign di " + gameObject.name);
            return;
        }

        // Mulai ke pointB
        targetPoint = pointB.position;
    }

    void Update()
    {
        if (pointA == null || pointB == null) return;
        if (isWaiting) return;

        // Gerak ke target
        Vector3 direction = (targetPoint - transform.position).normalized;
        direction.y = 0;

        transform.position += direction * moveSpeed * Time.deltaTime;

        // Rotate ke arah jalan
        if (direction != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRot,
                5f * Time.deltaTime
            );
        }

        // Cek apakah sudah sampai
        float distance = Vector3.Distance(
            new Vector3(transform.position.x, 0, transform.position.z),
            new Vector3(targetPoint.x, 0, targetPoint.z)
        );

        if (distance < 0.5f)
        {
            StartCoroutine(WaitAndTurn());
        }
    }

    System.Collections.IEnumerator WaitAndTurn()
    {
        isWaiting = true;

        yield return new WaitForSeconds(waitTime);

        // Balik arah
        if (targetPoint == pointB.position)
            targetPoint = pointA.position;
        else
            targetPoint = pointB.position;

        isWaiting = false;
    }
}