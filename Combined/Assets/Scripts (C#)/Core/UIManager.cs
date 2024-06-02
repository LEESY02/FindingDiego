using UnityEngine.SceneManagement;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;
    [SerializeField] private float SoundVolume;

    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;

    private void Awake() 
    {
        gameOverScreen.SetActive(false); //deactivate the gameOverScreen maually
        pauseScreen.SetActive(false); //deactivate the pauseScreen maually
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //if pause screen is already active unpause and viceversa
            if (pauseScreen.activeInHierarchy)
                PauseGame(false);
            else
                PauseGame(true);
        }
    }

    //activate game over screen
    #region  Game Over
    public void GameOver() 
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound, SoundVolume);
    }

    //game over functions
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
        Application.Quit(); //Quits the game (only works on build)

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //exits play mode in Unity (will only be executed in the editor)
        #endif
    }
    #endregion

    #region Pause
    private void PauseGame(bool status)
    {
        //if status == true pause | if status == false unpause
        pauseScreen.SetActive(status);

        if (status)
            Time.timeScale = 0; //pauses the time
        else
            Time.timeScale = 1; //time continues at 1x speed
    }
    #endregion
}
