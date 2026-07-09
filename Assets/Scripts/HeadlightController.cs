using UnityEngine;

public class HeadlightController : MonoBehaviour
{
    public Light leftLight;
    public Light rightLight;

    private bool headlightsOn = false;

    void Start()
    {
        SetLights(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            ToggleHeadlights();
        }
    }

    public void ToggleHeadlights()
    {
        headlightsOn = !headlightsOn;
        SetLights(headlightsOn);
    }

    void SetLights(bool state)
    {
        if (leftLight != null)
            leftLight.enabled = state;

        if (rightLight != null)
            rightLight.enabled = state;
    }
}