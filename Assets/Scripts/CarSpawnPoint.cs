using UnityEngine;

public class CarSpawnPoint : MonoBehaviour
{
    private Vector3 startPosition;
    private Quaternion startRotation;
    private Rigidbody rb;

    void Awake()
    {
        // Simpan di Awake agar lebih awal sebelum apapun berubah
        startPosition = transform.position;
        startRotation = transform.rotation;
        rb = GetComponent<Rigidbody>();

        Debug.Log("📍 Posisi awal tersimpan: " + startPosition);
    }

    public void ResetCar()
    {
        // Disable rigidbody dulu agar tidak ada physics interference
        if (rb != null) rb.isKinematic = true;

        transform.position = startPosition;
        transform.rotation = startRotation;

        if (rb != null)
        {
            rb.isKinematic = false;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        Debug.Log("🚗 Mobil direset ke: " + startPosition);
    }
}