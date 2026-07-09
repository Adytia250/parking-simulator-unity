using UnityEngine;

public class ParkingSystem : MonoBehaviour
{
    public GameManager gameManager;
    public int parkingScore = 0;
    public float maxAngle = 90f;
    public float maxVelocity = 2f;
    public ParticleSystem finishParticle;
    private bool success = false;

    void Start()
    {
        if (gameManager == null)
            Debug.LogError("❌ GameManager belum di-assign di ParkingSystem!");
    }

    void NextLevel()
    {
    gameManager.LoadNextLevel();
    }

    public void ResetParking()
    {
        success = false;
        parkingScore = 0;
    }

    private void OnTriggerStay(Collider other)
    {
        if (success || gameManager == null || !gameManager.isGameActive) return;

        Transform root = other.transform.root;

        if (root.CompareTag("Player"))
        {
            Rigidbody rb = root.GetComponent<Rigidbody>();
            if (rb == null) return;

            float speed = rb.linearVelocity.magnitude;
            float angle = Quaternion.Angle(root.rotation, transform.rotation);

           if (speed < maxVelocity && angle < maxAngle)
{
    parkingScore = 100;

    success = true;
    if (finishParticle != null)
{
    finishParticle.Play();
}
    gameManager.UpdateScore(parkingScore);
    RecordManager record =
    FindFirstObjectByType<RecordManager>();

    if(record != null)
    {
    record.StopTimer();

FinishAudio finishAudio =
    FindFirstObjectByType<FinishAudio>();

if(finishAudio != null)
{
    finishAudio.PlayFinish();
}
    }
if(record != null)
{
    record.StopTimer();

    FinishAudio finishAudio =
        FindFirstObjectByType<FinishAudio>();

    if(finishAudio != null)
    {
        finishAudio.PlayFinish();
    }
}

gameManager.ShowSuccess();

    Invoke(nameof(NextLevel), 3f);

    Debug.Log("✅ Parkir berhasil! Score: " + parkingScore);
}
        }
    }
}