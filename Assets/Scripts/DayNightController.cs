using UnityEngine;

public class DayNightController : MonoBehaviour
{
    public Light directionalLight;

    private bool isNight = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            ToggleDayNight();
        }
    }

    public void ToggleDayNight()
    {
        isNight = !isNight;

        if (isNight)
        {
            RenderSettings.ambientLight = Color.black;

            directionalLight.intensity = 0f;
        }
        else
        {
            RenderSettings.ambientLight = Color.white * 0.4f;

            directionalLight.intensity = 1f;
        }
    }
}