using UnityEngine;
using UnityEngine.Events;

public class Apple : MonoBehaviour
{
    [SerializeField] UnityEvent OnGet;
    private void OnTriggerEnter2D(Collider2D other)
    {
        OnGet?.Invoke();
        Destroy(gameObject);
    }
}
