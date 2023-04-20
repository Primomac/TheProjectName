using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLoopTest : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip musicIntro;

    private void Start()
    {
        if(musicIntro != null)
        {
            audioSource.PlayOneShot(musicIntro);
            audioSource.PlayScheduled(AudioSettings.dspTime + musicIntro.length);
        }
        else
            audioSource.PlayScheduled(AudioSettings.dspTime + 0);


    }
}
