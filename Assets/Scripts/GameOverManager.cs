using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverOverlay;
    private bool isGameOver = false;

    void Start()
    {
        gameOverOverlay.SetActive(false);
        Time.timeScale = 1f;
    }

    void Update()
    {
        // Check for space key only when game over overlay is shown
        if (isGameOver && Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1f;
            PlayAgain();
        }
    }

    public void ShowGameOver()
    {
        
        isGameOver = true;
        gameOverOverlay.SetActive(true);
        Time.timeScale = 0f;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
