using UnityEngine;

public class MiniMapFollow : MonoBehaviour
{
    public Transform player;

    void LateUpdate()
    {
        // Kamera hanya mengikuti posisi X dan Z pemain, ketinggian (Y) tetap
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        // Memastikan kamera tetap melihat ke bawah (tidak ikut miring)
        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}