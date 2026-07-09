using UnityEngine;

public class ArrowPulse : MonoBehaviour
{
    public float pulseSpeed = 2f;
    public float pulseScale = 0.15f;

    private Vector3 startScale;

    void Start()
    {
        startScale = transform.localScale;
    }

    void Update()
    {
        float scale =
            1 +
            Mathf.Sin(Time.time * pulseSpeed)
            * pulseScale;

        transform.localScale =
            startScale * scale;
    }
}