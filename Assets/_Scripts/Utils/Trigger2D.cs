using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger2D : MonoBehaviour
{
    public bool Triggered = false;
    [SerializeField] LayerMask layers;
    [SerializeField] UnityEvent OnTriggerEnter = new UnityEvent();
    [SerializeField] UnityEvent OnTriggerExit = new UnityEvent();

    private HashSet<Collider2D> mCollider2Ds = new HashSet<Collider2D>();

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(!LayerMaskUtility.Contains(layers, col.gameObject.layer)) return;

        mCollider2Ds.Add(col);

        if(!Triggered && mCollider2Ds.Count > 0)
        {
            Triggered = true;
            OnTriggerEnter?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if(!LayerMaskUtility.Contains(layers, col.gameObject.layer)) return;

        mCollider2Ds.Remove(col);

        if(Triggered && mCollider2Ds.Count == 0)
        {
            Triggered = false;
            OnTriggerExit?.Invoke();
        }
    }
}
