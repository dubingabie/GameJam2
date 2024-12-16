using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoSceneTransition : MonoBehaviour
{
    [SerializeField] string nextSceneName; // Name of the scene to load
    [SerializeField] VideoPlayer videoPlayer;

    void Start()
    {
        // If not assigned in inspector, try to get the VideoPlayer component
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        // Check if VideoPlayer is found
        if (videoPlayer != null)
        {
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
    
}