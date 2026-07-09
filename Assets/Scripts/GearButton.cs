using UnityEngine;

public class GearButton : MonoBehaviour
{
    public CarController carController;
    
    // Sesuaikan gearIndex: 0=Reverse, 1=Neutral, 2=Gigi1, 3=Gigi2, 4=Gigi3, 5=Gigi4, 6=Gigi5
    public int gearIndex; 

    public void OnButtonClick()
    {
        if (carController != null)
        {
            carController.SetGear(gearIndex);
            Debug.Log("Tombol diklik, mengganti ke Gear index: " + gearIndex);
        }
    }
}