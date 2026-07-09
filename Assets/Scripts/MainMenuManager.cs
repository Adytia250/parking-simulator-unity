using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Panel")]
    public GameObject howToPlayPanel;

    [Header("Audio")]
    public GameObject soundOnIcon;
    public GameObject soundOffIcon;
    public AudioSource menuMusic;

    private bool muted;

    void Start()
    {
        muted = PlayerPrefs.GetInt("Muted", 0) == 1;

        AudioListener.volume = muted ? 0f : 1f;

        UpdateSoundUI();

        if(howToPlayPanel != null)
            howToPlayPanel.SetActive(false);
        if(menuMusic != null)
{
    menuMusic.volume =
        muted ? 0f : 1f;
}
    }

    // =====================
    // PLAY
    // =====================

    public void PlayGame()
{
    if(menuMusic != null)
    {
        menuMusic.Stop();
    }

    LoadingManager.nextSceneIndex = 2;
    SceneManager.LoadScene(1);
}

    // =====================
    // EXIT
    // =====================

    public void ExitGame()
    {
        Debug.Log("Exit Game");

        Application.Quit();
    }

    // =====================
    // HOW TO PLAY
    // =====================

    public void OpenHowToPlay()
    {
        if(howToPlayPanel != null)
            howToPlayPanel.SetActive(true);
    }

    public void CloseHowToPlay()
    {
        if(howToPlayPanel != null)
            howToPlayPanel.SetActive(false);
    }

    // =====================
    // AUDIO
    // =====================

    public void ToggleAudio()
{
    muted = !muted;

    AudioListener.volume =
        muted ? 0f : 1f;

    if(menuMusic != null)
    {
        menuMusic.volume =
            muted ? 0f : 1f;
    }

    PlayerPrefs.SetInt(
        "Muted",
        muted ? 1 : 0
    );

    PlayerPrefs.Save();

    UpdateSoundUI();
}

    void UpdateSoundUI()
    {
        if(soundOnIcon != null)
            soundOnIcon.SetActive(!muted);

        if(soundOffIcon != null)
            soundOffIcon.SetActive(muted);
    }
}