using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Apple : MonoBehaviour
{
    [SerializeField] UnityEvent OnGet;

    private AudioSource getAudio;

    private void Awake()
    {
        getAudio = GetComponentInChildren<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        OnGet?.Invoke();
        getAudio.Play();
        StartCoroutine(Wait(getAudio.clip.length, () => Destroy(gameObject)));
    }

    IEnumerator Wait(float time, Action action)
    {
        yield return new WaitForSeconds(time);

        action?.Invoke();
    }
}
