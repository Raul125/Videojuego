using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;


    private void Awake()
    {
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }

        if (pauseScreen != null)
        {
            pauseScreen.SetActive(false);
        }
    }

    private void Update()
    {
        if (pauseScreen != null && Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame(!pauseScreen.activeInHierarchy);
        }
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.Instance.PlaySound(gameOverSound);
    }

    public void Restart()
    {
        gameOverScreen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        gameOverScreen.SetActive(false);
        SceneManager.LoadScene(0);
    }

    public void LoadLevel(int level)
    {
        int currentLevel = PlayerPrefs.GetInt("level", 1);
        if (level > currentLevel)
        {
            return;
        }

        SceneManager.LoadScene(level);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void PauseGame(bool status)
    {
        if (gameOverScreen.activeInHierarchy)
        {
            return;
        }

        pauseScreen.SetActive(status);

        Time.timeScale = status ? 0 : 1;
    }
    public void SoundVolume()
    {
        SoundManager.Instance.ChangeSoundVolume(0.2f);
    }

    public void MusicVolume()
    {
        SoundManager.Instance.ChangeMusicVolume(0.2f);
    }
}
