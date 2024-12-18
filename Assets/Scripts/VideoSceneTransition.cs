using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoSceneTransition : MonoBehaviour
{
    [SerializeField] string nextSceneName; // Name of the scene to load
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] TextMeshProUGUI loadngText;
    void Awake()
    {
        // If not assigned in inspector, try to get the VideoPlayer component
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        // Check if VideoPlayer is found
        if (videoPlayer != null)
        {
            videoPlayer.started += OnVideoStarted;
            // disable the loading text
            // Subscribe to the event that triggers when the video finishes
            videoPlayer.loopPointReached += OnVideoFinished;
        }
        else
        {
            Debug.LogError("No VideoPlayer component found!");
        }
    }

    // Event handler called when the video finishes
    void OnVideoFinished(VideoPlayer source)
    {
        // Load the next scene
        SceneManager.LoadScene(nextSceneName);
    }
    void OnVideoStarted(VideoPlayer source)
    {
        loadngText.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
    
}