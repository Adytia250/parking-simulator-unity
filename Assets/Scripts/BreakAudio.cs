using UnityEngine;

public class BrakeAudio : MonoBehaviour
{
    public AudioClip brakeClip;

    private bool played = false;

    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            if (!played)
            {
                played = true;

                if (brakeClip != null)
                {
                    AudioSource.PlayClipAtPoint(
                        brakeClip,
                        Camera.main.transform.position
                    );
                }
            }
        }
        else
        {
            played = false;
        }
    }
}