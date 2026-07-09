using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GasIndicator : MonoBehaviour
{
    public CarController carController;
    public Slider gasSlider;
    public TMP_Text speedText;

    void Update()
    {
        if (carController == null) return;

        float speed = carController.GetCurrentSpeed();

        gasSlider.value = speed / carController.GetMaxSpeed();

        speedText.text =
            Mathf.RoundToInt(speed * 10f) + " Km/H";
    }
}