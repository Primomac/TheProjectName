using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLoopTest : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip musicIntro;

    private void Start()
    {
        audioSource.PlayOneShot(musicIntro);
        audioSource.PlayScheduled(AudioSettings.dspTime + musicIntro.length);
    }
}
