using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayAudioSource : MonoBehaviour
{
    public UnityEvent OnPlayFinish;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Excute()
    {
        audioSource.Play();
        Invoke(nameof(OnFinish), audioSource.clip.length);
    }

    private void OnFinish()
    {
        OnPlayFinish?.Invoke();
    }
}
