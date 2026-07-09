using UnityEngine;
using TMPro;

public class RecordManager : MonoBehaviour
{
    public TMP_Text yourRecordText;
    public TMP_Text bestRecordText;

    public float startTime = 60f;

    private float currentTime;
    private bool running = true;

    void Start()
    {
        currentTime = startTime;

        float bestTime =
            PlayerPrefs.GetFloat("BestTime", 9999f);

        if (bestTime < 9999f)
        {
            bestRecordText.text =
                bestTime.ToString("F2") + " s";
        }
        else
        {
            bestRecordText.text = "--";
        }
    }

    void Update()
    {
        if (!running)
            return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            currentTime = 0;
            running = false;

            GameManager gm =
                FindFirstObjectByType<GameManager>();

            if (gm != null)
            {
                GameOverAudio gameOverAudio =
                    FindFirstObjectByType<GameOverAudio>();

                if (gameOverAudio != null)
                {
                    gameOverAudio.PlayGameOver();
                }

                gm.ShowGameOver();
            }
        }

        yourRecordText.text =
            currentTime.ToString("F2") + " s";
    }

    public void StopTimer()
    {
        running = false;

        float usedTime =
            startTime - currentTime;

        float bestTime =
            PlayerPrefs.GetFloat("BestTime", 9999f);

        if (usedTime < bestTime)
        {
            PlayerPrefs.SetFloat(
                "BestTime",
                usedTime);

            bestTime = usedTime;
        }

        bestRecordText.text =
            bestTime.ToString("F2") + " s";
    }

    public void ResetTimer()
    {
        currentTime = startTime;
        running = true;

        yourRecordText.text =
            currentTime.ToString("F2") + " s";
    }

    public bool IsTimeOver()
    {
        return currentTime <= 0;
    }
}