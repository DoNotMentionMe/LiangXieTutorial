using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Apple : MonoBehaviour
{
    [SerializeField] UnityEvent OnGet;

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnGet?.Invoke();
        StartCoroutine(Wait(0.5f, () => Destroy(gameObject)));
    }

    IEnumerator Wait(float time, Action action)
    {
        yield return new WaitForSeconds(time);

        action?.Invoke();
    }
}
