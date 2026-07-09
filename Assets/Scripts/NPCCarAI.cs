using UnityEngine;

public class NPCCarAI : MonoBehaviour
{
    public Transform[] waypoints;
    public float moveSpeed = 5f;
    public float turnSpeed = 3f;
    public float waypointDistance = 5f;
    public int startWaypoint = 0;

    private int currentWaypoint = 0;

    void Start()
    {
        if (waypoints == null || waypoints.Length == 0)
        {
            Debug.LogError("❌ Waypoints belum di-assign di " + gameObject.name);
            return;
        }

        currentWaypoint = startWaypoint % waypoints.Length;
    }

    void Update()
    {
        if (waypoints == null || waypoints.Length == 0)
            return;

        Transform target = waypoints[currentWaypoint];

        Vector3 direction = target.position - transform.position;
        direction.y = 0;

        if (direction.magnitude > 0.1f)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(direction);

            transform.rotation =
                Quaternion.Slerp(
                    transform.rotation,
                    targetRotation,
                    turnSpeed * Time.deltaTime
                );
        }

        transform.Translate(
            Vector3.forward *
            moveSpeed *
            Time.deltaTime
        );

        float distance =
            new Vector3(
                target.position.x - transform.position.x,
                0,
                target.position.z - transform.position.z
            ).magnitude;

        if (distance < waypointDistance)
        {
            currentWaypoint =
                (currentWaypoint + 1) %
                waypoints.Length;
        }
    }
}