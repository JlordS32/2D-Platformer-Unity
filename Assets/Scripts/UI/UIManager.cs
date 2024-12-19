using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header ("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    [Header ("Pause")]
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private AudioClip pauseSound;

    private void Awake()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (pauseScreen.activeInHierarchy) {
                Pause(false);
            } else {
                Pause(true);
            }
        }
    }

    #region GAMEOVER
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.playSound(gameOverSound);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();

        // Some stuff here

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    #endregion

    #region PAUSE
    public void Pause(bool status)
    {
        pauseScreen.SetActive(status);
        SoundManager.instance.playSound(pauseSound);

        Time.timeScale = System.Convert.ToInt32(!status);
    }

    public void SoundVolume() {
        
        SoundManager.instance.ChangeSoundVolume(0.2f);
    }

    public void MusicVolume() {
        SoundManager.instance.ChangeMusicVolume(0.2f);
    }

    #endregion
}
