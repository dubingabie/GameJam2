using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionsManager : MonoBehaviour
{
    public GameObject instructionsOverlay;
    [SerializeField] private GameObject backgroundMusicPlayer;
    private BackgroundMusic backgroundMusic;
    void Start()
    {
        //instructionsOverlay.SetActive(true);
        //Time.timeScale = 0f;
        GameState.isGamePaused = true;
        instructionsOverlay.SetActive(true);
        Time.timeScale = 0f;
        backgroundMusic =
            backgroundMusicPlayer.GetComponent<BackgroundMusic>();
        backgroundMusic.stopMusic();
    }

    void Update()
    {
        
        // Check for space key only when game over overlay is shown
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Play();
        }
    }
    public void ShowInstructions()
    {
        instructionsOverlay.SetActive(true);
        Time.timeScale = 0f;
        backgroundMusic.stopMusic();
    }
    public void Play()
    {
        
        backgroundMusic.startMusic();
        GameState.isGamePaused = false;
        instructionsOverlay.SetActive(false);
        Time.timeScale = 1f;
    }

    // public void PlayAgain()
    // {
    //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    // }
}