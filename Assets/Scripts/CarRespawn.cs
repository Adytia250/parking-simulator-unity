using UnityEngine;
using System.Collections;

public class CarRespawn : MonoBehaviour
{
    public GameManager gameManager;

    [Header("Respawn")]
    public float respawnDelay = 1.5f;

    public string[] respawnTags =
    {
        "NPC",
        "Wall",
        "Obstacle"
    };

    private Vector3 spawnPosition;
    private Quaternion spawnRotation;

    private Rigidbody rb;
    private bool isRespawning = false;

    private Renderer[] carRenderers;
    private Material[] materials;

    void Start()
    {
        spawnPosition = transform.position;
        spawnRotation = transform.rotation;

        rb = GetComponent<Rigidbody>();

        carRenderers =
            GetComponentsInChildren<Renderer>();

        materials =
            GetComponentsInChildren<Renderer>()[0]
            .materials;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!CompareTag("Player"))
            return;

        if (isRespawning)
            return;

        if (gameManager != null &&
            !gameManager.isGameActive)
            return;

        foreach (string tag in respawnTags)
        {
            if (collision.gameObject.CompareTag(tag))
            {
                CrashAudio crashAudio =
                    FindFirstObjectByType<CrashAudio>();

                if (crashAudio != null)
                {
                    crashAudio.PlayCrash();
                }

                StartCoroutine(RespawnCar());

                break;
            }
        }
    }

    IEnumerator RespawnCar()
    {
        isRespawning = true;

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        yield return StartCoroutine(FlashRedEffect());

        transform.position = spawnPosition;
        transform.rotation = spawnRotation;

        if (rb != null)
        {
            rb.isKinematic = false;

            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        SetRenderersVisible(true);

        isRespawning = false;

        Debug.Log("🔄 Mobil respawn!");
    }

    IEnumerator FlashRedEffect()
    {
        float elapsed = 0f;

        while (elapsed < respawnDelay)
        {
            foreach (Renderer rend in carRenderers)
            {
                if (rend == null)
                    continue;

                foreach (Material mat in rend.materials)
                {
                    mat.color = Color.red;
                }
            }

            yield return new WaitForSeconds(0.1f);

            foreach (Renderer rend in carRenderers)
            {
                if (rend == null)
                    continue;

                foreach (Material mat in rend.materials)
                {
                    mat.color = Color.white;
                }
            }

            yield return new WaitForSeconds(0.1f);

            elapsed += 0.2f;
        }
    }

    void SetRenderersVisible(bool visible)
    {
        foreach (Renderer r in carRenderers)
        {
            if (r != null)
            {
                r.enabled = visible;
            }
        }
    }

    public void ResetSpawnPoint(
        Vector3 newPos,
        Quaternion newRot)
    {
        spawnPosition = newPos;
        spawnRotation = newRot;
    }
}