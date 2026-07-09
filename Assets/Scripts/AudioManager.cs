using UnityEngine;

public class AudioManager : MonoBehaviour
{
    bool muted = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            muted = !muted;

            AudioListener.volume =
                muted ? 0f : 1f;
        }
    }
}