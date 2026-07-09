using UnityEngine;
using TMPro;

public class CarController : MonoBehaviour
{
    [Header("Movement")]
    public float acceleration = 8f;
    public float brakeForce = 20f;
    public float turnSpeed = 90f;

    [Header("Realistic Movement")]
    public float dragForce = 2.5f;

    [Header("Gear Speed")]
    public float reverseMaxSpeed = 4f;
    public float gear1MaxSpeed = 8f;
    public float gear2MaxSpeed = 14f;
    public float gear3MaxSpeed = 20f;
    public float gear4MaxSpeed = 28f;
    public float gear5MaxSpeed = 40f;

    [Header("UI")]
    public TMP_Text gearText;

    public GameManager gameManager;

    private float currentSpeed = 0f;

    public enum Gear
    {
        Reverse,
        Neutral,
        Forward1,
        Forward2,
        Forward3,
        Forward4,
        Forward5
    }

    public Gear currentGear = Gear.Neutral;

    void Start()
    {
        UpdateGearUI();
    }

    void Update()
    {
        if (gameManager != null && !gameManager.isGameActive)
            return;

        HandleKeyboardGearInput();
        HandleMovement();
        UpdateGearUI();
    }

    public void SetGear(int gearIndex)
    {
        currentGear = (Gear)gearIndex;
        Debug.Log("Gear : " + currentGear);
    }

    public float GetCurrentSpeed()
    {
        return Mathf.Abs(currentSpeed);
    }

    public float GetMaxSpeed()
    {
        return gear5MaxSpeed;
    }

    void HandleKeyboardGearInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SetGear(2);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            SetGear(3);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            SetGear(4);

        if (Input.GetKeyDown(KeyCode.Alpha4))
            SetGear(5);

        if (Input.GetKeyDown(KeyCode.Alpha5))
            SetGear(6);

        if (Input.GetKeyDown(KeyCode.N))
            SetGear(1);

        if (Input.GetKeyDown(KeyCode.R))
            SetGear(0);
    }

    void HandleMovement()
    {
        float turn = 0;

        if (Input.GetKey(KeyCode.LeftArrow) ||
            (MobileInput.Instance != null &&
             MobileInput.Instance.leftPressed))
        {
            turn = -1;
        }

        if (Input.GetKey(KeyCode.RightArrow) ||
            (MobileInput.Instance != null &&
             MobileInput.Instance.rightPressed))
        {
            turn = 1;
        }

        float maxAllowedSpeed = GetGearMaxSpeed();

        bool gasPressed =
    Input.GetKey(KeyCode.UpArrow) ||
            (MobileInput.Instance != null &&
             MobileInput.Instance.gasPressed);

        bool brakePressed =
    Input.GetKey(KeyCode.DownArrow) ||
            (MobileInput.Instance != null &&
             MobileInput.Instance.brakePressed);

        // GAS
        if (gasPressed)
        {
            if (currentGear == Gear.Reverse)
            {
                currentSpeed -=
                    acceleration *
                    Time.deltaTime;
            }
            else if (currentGear != Gear.Neutral)
            {
                currentSpeed +=
                    acceleration *
                    GetGearAccelerationMultiplier() *
                    Time.deltaTime;
            }
        }

        // REM
        if (brakePressed)
        {
            currentSpeed = Mathf.MoveTowards(
                currentSpeed,
                0,
                brakeForce * Time.deltaTime
            );
        }

        // DRAG
        if (!gasPressed && !brakePressed)
        {
            currentSpeed = Mathf.MoveTowards(
                currentSpeed,
                0,
                dragForce * Time.deltaTime
            );
        }

        // BATAS KECEPATAN
        if (currentGear == Gear.Reverse)
        {
            currentSpeed = Mathf.Clamp(
                currentSpeed,
                -reverseMaxSpeed,
                0
            );
        }
        else
        {
            currentSpeed = Mathf.Clamp(
                currentSpeed,
                0,
                maxAllowedSpeed
            );
        }

        // GERAK
        transform.Translate(
            Vector3.forward *
            currentSpeed *
            Time.deltaTime
        );

        // BELOK
        if (Mathf.Abs(currentSpeed) > 0.1f)
        {
            float speedFactor =
                Mathf.Clamp01(
                    GetCurrentSpeed() /
                    GetMaxSpeed()
                );

            float steeringMultiplier =
                Mathf.Lerp(
                    1.5f,
                    0.4f,
                    speedFactor
                );

            transform.Rotate(
                Vector3.up *
                turn *
                turnSpeed *
                steeringMultiplier *
                Time.deltaTime
            );
        }
    }

    float GetGearAccelerationMultiplier()
    {
        switch (currentGear)
        {
            case Gear.Forward1: return 1.4f;
            case Gear.Forward2: return 1.2f;
            case Gear.Forward3: return 1.0f;
            case Gear.Forward4: return 0.8f;
            case Gear.Forward5: return 0.6f;
            default: return 1f;
        }
    }

    float GetGearMaxSpeed()
    {
        switch (currentGear)
        {
            case Gear.Forward1: return gear1MaxSpeed;
            case Gear.Forward2: return gear2MaxSpeed;
            case Gear.Forward3: return gear3MaxSpeed;
            case Gear.Forward4: return gear4MaxSpeed;
            case Gear.Forward5: return gear5MaxSpeed;
            default: return 0f;
        }
    }

    void UpdateGearUI()
    {
        if (gearText == null)
            return;

        switch (currentGear)
        {
            case Gear.Reverse: gearText.text = "R"; break;
            case Gear.Neutral: gearText.text = "N"; break;
            case Gear.Forward1: gearText.text = "1"; break;
            case Gear.Forward2: gearText.text = "2"; break;
            case Gear.Forward3: gearText.text = "3"; break;
            case Gear.Forward4: gearText.text = "4"; break;
            case Gear.Forward5: gearText.text = "5"; break;
        }
    }
}