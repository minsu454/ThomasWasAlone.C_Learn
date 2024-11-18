using Common.Timer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    private float delay;

    private void OnEnable()
    {
        StartCoroutine(CoTimer.Start(delay, () => gameObject.SetActive(false)));
    }

    public void SetDelay(float delay, bool is3D = true)
    {
        this.delay = delay;
        
    }

    public void SetSound2D()
    {
        audioSource.spatialBlend = 0;
    }

    public void SetSound3D()
    {
        audioSource.spatialBlend = 1;
    }

    public void Play(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
