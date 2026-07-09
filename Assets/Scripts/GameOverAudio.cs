using UnityEngine;

public class GameOverAudio : MonoBehaviour
{
    public AudioClip gameOverClip;

    public void PlayGameOver()
    {
        if (gameOverClip == null)
            return;

        AudioSource.PlayClipAtPoint(
            gameOverClip,
            Camera.main.transform.position
        );
    }
}