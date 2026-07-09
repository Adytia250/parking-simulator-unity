using UnityEngine;
using UnityEngine.UI;

public class ArrowGuide : MonoBehaviour
{
    public Transform player;
    public Transform parkingTarget;

    public float smoothSpeed = 5f;

    private RectTransform rect;

    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (player == null || parkingTarget == null)
            return;

        Vector3 direction =
            parkingTarget.position -
            player.position;

        direction.y = 0;

        float angle =
            Vector3.SignedAngle(
                player.forward,
                direction,
                Vector3.up
            );

        Quaternion targetRotation =
            Quaternion.Euler(
                0,
                0,
                -angle
            );

        rect.rotation =
            Quaternion.Lerp(
                rect.rotation,
                targetRotation,
                smoothSpeed * Time.deltaTime
            );
    }
}