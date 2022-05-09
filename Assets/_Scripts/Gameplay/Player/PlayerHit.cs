using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHit : MonoBehaviour
{
    [SerializeField] UnityEvent OnHit = new UnityEvent();

    public void Hit()
    {
        OnHit?.Invoke();
    }
}
