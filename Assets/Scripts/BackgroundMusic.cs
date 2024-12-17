using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private AudioClip musicClip;
    [SerializeField][Range(0.0f, 3.0f)] private float volume = 1.0f;
    [SerializeField] private float loopStartTime = 2f; // Time in seconds where loop should start
    [SerializeField] private float loopEndTime = 29f;   
        
    private AudioSource audioSource;
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = musicClip;
        audioSource.loop = true;
        audioSource.volume = volume;
        audioSource.Play();
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
}