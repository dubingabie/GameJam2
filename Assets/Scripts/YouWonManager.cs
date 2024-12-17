using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouWonManager : MonoBehaviour
{
    public GameObject YouWonOverlay;
    private bool isYouWon = false;
    [SerializeField] private GameObject backgroundMusic;
    void Start()
    {
        YouWonOverlay.SetActive(false);
        Time.timeScale = 1f;
    }

    void Update()
    {
        // Check for space key only when game over overlay is shown
        if (isYouWon && Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1f;
            PlayAgain();
        }
    }

    public void ShowYouWon()
    {
        backgroundMusic.GetComponent<BackgroundMusic>().stopMusic();
        isYouWon = true;
        YouWonOverlay.SetActive(true);
        Time.timeScale = 0f;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
