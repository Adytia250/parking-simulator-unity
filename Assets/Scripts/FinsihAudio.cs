using UnityEngine;

public class FinishAudio : MonoBehaviour
{
    public AudioClip finishClip;

    public void PlayFinish()
    {
        if (finishClip == null)
            return;

        AudioSource.PlayClipAtPoint(
            finishClip,
            Camera.main.transform.position
        );
    }
}