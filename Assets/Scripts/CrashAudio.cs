using UnityEngine;

public class CrashAudio : MonoBehaviour
{
    public AudioClip crashClip;

    public void PlayCrash()
    {
        if (crashClip == null)
            return;

        AudioSource.PlayClipAtPoint(
            crashClip,
            Camera.main.transform.position
        );
    }
}