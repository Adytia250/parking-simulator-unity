using UnityEngine;

public class MobileInput : MonoBehaviour
{
    public static MobileInput Instance;

    [HideInInspector] public bool gasPressed;
    [HideInInspector] public bool brakePressed;
    [HideInInspector] public bool leftPressed;
    [HideInInspector] public bool rightPressed;

    void Awake()
    {
        Instance = this;
    }
}