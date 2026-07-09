using UnityEngine;

public class CarHeadLight : MonoBehaviour
{
    public Light leftHeadLight;
    public Light rightHeadLight;
    public Light rearLight;

    void Start()
    {
        // Lampu nyala otomatis saat game mulai
        if (leftHeadLight != null) leftHeadLight.enabled = true;
        if (rightHeadLight != null) rightHeadLight.enabled = true;
        if (rearLight != null) rearLight.enabled = true;
    }
}