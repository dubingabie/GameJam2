using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private AudioClip backgroundMusicClip;
    [SerializeField][Range(0.0f, 3.0f)] private float backgroundMusicVolume = 1.0f;
    [SerializeField] private float loopStartTime = 2f; // Time in seconds where loop should start
    [SerializeField] private float loopEndTime = 29f;   
    private AudioSource audioSource;
    private bool gameOver = false;
    [SerializeField] private AudioClip youWonMusicClip;
    [SerializeField][Range(0.0f, 3.0f)] private float youWonMusicVolume = 1.0f;
    [SerializeField] private AudioClip gameOverMusicClip;
    [SerializeField][Range(0.0f, 3.0f)] private float gameOverMusicVolume = 1.0f;
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        startMusic();
    }
    
    // Update is called once per frame
    void Update()
    {
        // Check if we've reached the loop end point
        if (audioSource.time >= loopEndTime)
        {
            // Jump back to loop start point
            audioSource.time = loopStartTime;
        }
    }

    public void stopMusic()
    {
        audioSource.Stop();
    }

    public void startGameOverMusic()
    {
     loopStartTime = 0f; // Time in seconds where loop should start
     loopEndTime = 69f;
     audioSource.volume = gameOverMusicVolume;
     audioSource.clip = gameOverMusicClip;
     audioSource.Play();
    }
    public void startYouWonMusic()
    {
        audioSource.volume = youWonMusicVolume;
        audioSource.clip = youWonMusicClip;
        audioSource.Play();
    }
    public void startMusic()
    {   
        audioSource.clip = backgroundMusicClip;
        audioSource.loop = true;
        audioSource.volume = backgroundMusicVolume;
        audioSource.loop = false;
        audioSource.Play();
        
    }
}