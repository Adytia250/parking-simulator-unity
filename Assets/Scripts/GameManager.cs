using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject successPanel;
    public GameObject gameOverPanel;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI nextLevelText;

    [Header("Player")]
    public CarSpawnPoint carSpawnPoint;

    [HideInInspector]
    public bool isGameActive = true;

    void Awake()
    {
        isGameActive = true;

        Time.timeScale = 1f;

        if (successPanel != null)
            successPanel.SetActive(false);

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    void Update()
{
    if (!isGameActive)
    {
        // Level 1 -> Level 2
        if (
            successPanel != null &&
            successPanel.activeSelf &&
            SceneManager.GetActiveScene().buildIndex == 0 &&
            Input.GetKeyDown(KeyCode.N)
        )
        {
            LoadNextLevel();
        }

        // Level 2 -> Level 1
        if (
            successPanel != null &&
            successPanel.activeSelf &&
            SceneManager.GetActiveScene().buildIndex == 1 &&
            Input.GetKeyDown(KeyCode.B)
        )
        {
            SceneManager.LoadScene(0);
        }
    }
}

    // ==========================
    // SUCCESS
    // ==========================

    public void ShowSuccess()
    {
        if (!isGameActive) return;

        isGameActive = false;

        if (successPanel != null)
            successPanel.SetActive(true);

        // Stop record timer
        RecordManager record =
            FindFirstObjectByType<RecordManager>();

        if (record != null)
            record.StopTimer();

        int currentLevel =
            SceneManager.GetActiveScene().buildIndex;

        if (nextLevelText != null)
        {
            if (currentLevel == 0)
            {
                nextLevelText.text =
                    "PARKIR BERHASIL!\n\nTekan N untuk lanjut ke Level 2";
            }
            else
            {
                nextLevelText.text =
                    "PARKIR BERHASIL!\n\nTekan B untuk kembali ke Level 1";
            }
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("✅ PARKIR BERHASIL");
    }

    // ==========================
    // GAME OVER
    // ==========================

    public void ShowGameOver()
    {
        if (!isGameActive) return;

        isGameActive = false;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("❌ GAME OVER");
    }

    // ==========================
    // RESTART
    // ==========================

public void RestartGame()
{
    Time.timeScale = 1f;

    PauseMenu pause =
        FindFirstObjectByType<PauseMenu>();

    if (pause != null)
    {
        pause.pausePanel.SetActive(false);
    }

    isGameActive = true;

    if (successPanel != null)
        successPanel.SetActive(false);

    if (gameOverPanel != null)
        gameOverPanel.SetActive(false);

    if (carSpawnPoint != null)
        carSpawnPoint.ResetCar();

    RecordManager record =
        FindFirstObjectByType<RecordManager>();

    if (record != null)
        record.ResetTimer();

    UpdateScore(0);

    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
}
    // ==========================
    // SCORE
    // ==========================

    public void UpdateScore(int score)
    {
        if (scoreText != null)
        {
            scoreText.text =
                "Score : " + score;
        }
    }

    // ==========================
    // LEVEL
    // ==========================

    public void LoadNextLevel()
    {
        int currentScene =
            SceneManager.GetActiveScene().buildIndex;

        if (
            currentScene + 1 <
            SceneManager.sceneCountInBuildSettings
        )
        {
            SceneManager.LoadScene(currentScene + 1);
        }
        else
        {
            Debug.Log("🏆 Semua level selesai!");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    public void ExitToMainMenu()
{
    Time.timeScale = 1f;
    SceneManager.LoadScene("Main Menu");
}
}