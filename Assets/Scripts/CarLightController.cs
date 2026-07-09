using UnityEngine;

public class CarLightController : MonoBehaviour
{
    [Header("Brake Lights")]
    public Light brakeLightLeft;
    public Light brakeLightRight;

    [Header("Reverse Lights")]
    public Light reverseLightLeft;
    public Light reverseLightRight;

    public CarController carController;

    void Update()
    {
        HandleBrakeLights();
        HandleReverseLights();
    }

    void HandleBrakeLights()
    {
        bool braking =
            Input.GetKey(KeyCode.S) ||
            (MobileInput.Instance != null &&
             MobileInput.Instance.brakePressed);

        float intensity =
            braking ? 8f : 0f;

        brakeLightLeft.intensity = intensity;
        brakeLightRight.intensity = intensity;
    }

    void HandleReverseLights()
    {
        bool reverse =
            carController.currentGear ==
            CarController.Gear.Reverse;

        float intensity =
            reverse ? 5f : 0f;

        reverseLightLeft.intensity = intensity;
        reverseLightRight.intensity = intensity;
    }
}